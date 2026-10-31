package com.karlitodev.forge.ui.main

import android.content.Context
import android.content.Intent
import android.graphics.Bitmap
import android.graphics.Canvas
import android.graphics.Color
import android.graphics.Paint
import android.graphics.RectF
import android.graphics.Typeface
import android.text.Layout
import android.text.StaticLayout
import android.text.TextPaint
import androidx.core.content.FileProvider
import com.karlitodev.forge.data.local.QuoteEntity
import java.io.File
import java.io.FileOutputStream
import java.security.MessageDigest

object WatermarkedQuoteSharing {

    fun shareWatermarkedQuote(
        context: Context,
        quote: QuoteEntity,
        userId: String
    ) {
        val bitmap = createQuoteBitmap(quote, userId)
        val imageUri = saveBitmapToCache(context, bitmap)

        val intent = Intent(Intent.ACTION_SEND).apply {
            type = "image/png"
            putExtra(Intent.EXTRA_STREAM, imageUri)
            addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION)
        }

        val chooser = Intent.createChooser(intent, "Partager la citation Forge")
        chooser.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK)
        context.startActivity(chooser)
    }

    private fun createQuoteBitmap(quote: QuoteEntity, userId: String): Bitmap {
        val width = 1080
        val height = 1350
        val bitmap = Bitmap.createBitmap(width, height, Bitmap.Config.ARGB_8888)
        val canvas = Canvas(bitmap)

        // 1. Fond #F5F1E8
        canvas.drawColor(Color.parseColor("#F5F1E8"))

        // 2. Bordure intérieure élégante
        val borderPaint = Paint().apply {
            color = Color.parseColor("#DCD4C6")
            style = Paint.Style.STROKE
            strokeWidth = 4f
            isAntiAlias = true
        }
        val borderRect = RectF(60f, 60f, width - 60f, height - 60f)
        canvas.drawRoundRect(borderRect, 24f, 24f, borderPaint)

        // 3. En-tête : FORGE
        val headerPaint = Paint().apply {
            color = Color.parseColor("#121212")
            textSize = 42f
            letterSpacing = 0.25f
            typeface = Typeface.create(Typeface.SERIF, Typeface.BOLD)
            textAlign = Paint.Align.CENTER
            isAntiAlias = true
        }
        canvas.drawText("FORGE", width / 2f, 180f, headerPaint)

        val subHeaderPaint = Paint().apply {
            color = Color.parseColor("#6B665F")
            textSize = 24f
            letterSpacing = 0.15f
            typeface = Typeface.create(Typeface.DEFAULT, Typeface.NORMAL)
            textAlign = Paint.Align.CENTER
            isAntiAlias = true
        }
        canvas.drawText("DISCIPLINE STOÏCIENNE", width / 2f, 230f, subHeaderPaint)

        // 4. Badge Jour
        val dayBadgePaint = Paint().apply {
            color = Color.parseColor("#121212")
            textSize = 26f
            letterSpacing = 0.1f
            typeface = Typeface.create(Typeface.SERIF, Typeface.BOLD)
            textAlign = Paint.Align.CENTER
            isAntiAlias = true
        }
        canvas.drawText("— JOUR ${quote.day} —", width / 2f, 400f, dayBadgePaint)

        // 5. Texte de la citation (mise en page multi-lignes propre)
        val quoteTextPaint = TextPaint().apply {
            color = Color.parseColor("#121212")
            textSize = 52f
            typeface = Typeface.create(Typeface.SERIF, Typeface.NORMAL)
            isAntiAlias = true
        }

        val textWidth = (width - 240).coerceAtLeast(100)
        val textWithQuotes = "« ${quote.text} »"
        val staticLayout = StaticLayout.Builder.obtain(
            textWithQuotes,
            0,
            textWithQuotes.length,
            quoteTextPaint,
            textWidth
        )
            .setAlignment(Layout.Alignment.ALIGN_CENTER)
            .setLineSpacing(16f, 1.25f)
            .setIncludePad(false)
            .build()

        canvas.save()
        val textHeight = staticLayout.height
        val startY = ((height - textHeight) / 2f).coerceAtLeast(460f)
        canvas.translate(120f, startY)
        staticLayout.draw(canvas)
        canvas.restore()

        // 6. Filigrane invisible (8% d'opacité en bas à droite) : Logo Forge + Hash UserID
        // Ingrédients invisibles à l'œil nu mais détectables numériquement / par IA
        val hashedUserId = sha256(userId.ifBlank { "forge_anonymous_user" }).take(16)
        val watermarkText = "FORGE · $hashedUserId"

        val watermarkPaint = Paint().apply {
            // Opacité 8% = 20 sur 255
            color = Color.argb(20, 18, 18, 18)
            textSize = 22f
            letterSpacing = 0.1f
            typeface = Typeface.create(Typeface.MONOSPACE, Typeface.NORMAL)
            textAlign = Paint.Align.RIGHT
            isAntiAlias = true
        }
        canvas.drawText(watermarkText, width - 90f, height - 90f, watermarkPaint)

        return bitmap
    }

    private fun sha256(input: String): String {
        val bytes = MessageDigest.getInstance("SHA-256").digest(input.toByteArray(Charsets.UTF_8))
        return bytes.joinToString("") { "%02x".format(it) }
    }

    private fun saveBitmapToCache(context: Context, bitmap: Bitmap): android.net.Uri {
        val cacheDir = File(context.cacheDir, "shared_quotes").apply { mkdirs() }
        val file = File(cacheDir, "quote_${System.currentTimeMillis()}.png")

        FileOutputStream(file).use { out ->
            bitmap.compress(Bitmap.CompressFormat.PNG, 100, out)
        }

        return FileProvider.getUriForFile(
            context,
            "com.karlitodev.forge.fileprovider",
            file
        )
    }
}
