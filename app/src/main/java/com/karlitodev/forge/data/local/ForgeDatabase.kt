package com.karlitodev.forge.data.local

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import androidx.sqlite.db.SupportSQLiteDatabase
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Database(
    entities = [QuoteEntity::class, UserEntity::class],
    version = 1,
    exportSchema = false
)
abstract class ForgeDatabase : RoomDatabase() {

    abstract fun quoteDao(): QuoteDao
    abstract fun userDao(): UserDao

    companion object {
        @Volatile
        private var INSTANCE: ForgeDatabase? = null

        fun getInstance(context: Context): ForgeDatabase {
            return INSTANCE ?: synchronized(this) {
                val instance = Room.databaseBuilder(
                    context.applicationContext,
                    ForgeDatabase::class.java,
                    "forge_database.db"
                )
                    .addCallback(object : Callback() {
                        override fun onCreate(db: SupportSQLiteDatabase) {
                            super.onCreate(db)
                            // Populate 21 initial hardcoded quotes on creation
                            CoroutineScope(Dispatchers.IO).launch {
                                getInstance(context).quoteDao().insertAll(InitialQuotes.items)
                            }
                        }
                    })
                    .build()
                INSTANCE = instance
                instance
            }
        }
    }
}
