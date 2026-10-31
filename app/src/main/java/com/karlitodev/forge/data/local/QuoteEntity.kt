package com.karlitodev.forge.data.local

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "quotes")
data class QuoteEntity(
    @PrimaryKey(autoGenerate = true)
    val id: Long = 0,
    val text: String,
    val lang: String,
    val day: Int,
    val isSeen: Boolean = false
)
