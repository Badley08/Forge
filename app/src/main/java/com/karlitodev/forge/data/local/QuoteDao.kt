package com.karlitodev.forge.data.local

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query

@Dao
interface QuoteDao {

    @Query("SELECT * FROM quotes WHERE isSeen = 0 AND lang = :lang ORDER BY day ASC, id ASC LIMIT 1")
    suspend fun getNextUnseenQuote(lang: String): QuoteEntity?

    @Query("UPDATE quotes SET isSeen = 1 WHERE id = :quoteId")
    suspend fun markQuoteAsSeen(quoteId: Long)

    @Query("SELECT COUNT(*) FROM quotes WHERE isSeen = 0 AND lang = :lang")
    suspend fun countUnseenQuotes(lang: String): Int

    @Query("SELECT COUNT(*) FROM quotes")
    suspend fun countTotalQuotes(): Int

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertAll(quotes: List<QuoteEntity>)

    @Query("UPDATE quotes SET isSeen = 0")
    suspend fun resetAllQuotes()
}
