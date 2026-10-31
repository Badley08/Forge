package com.karlitodev.forge.data.repository

import com.karlitodev.forge.data.billing.BillingManager
import com.karlitodev.forge.data.local.ForgeDatabase
import com.karlitodev.forge.data.local.InitialQuotes
import com.karlitodev.forge.data.local.QuoteEntity
import com.karlitodev.forge.data.local.UserEntity
import com.karlitodev.forge.data.preferences.UserPreferencesRepository
import kotlinx.coroutines.flow.Flow

class ForgeRepository(
    private val database: ForgeDatabase,
    private val preferencesRepository: UserPreferencesRepository,
    val billingManager: BillingManager
) {

    private val quoteDao = database.quoteDao()
    private val userDao = database.userDao()

    val userNameFlow: Flow<String> = preferencesRepository.userNameFlow
    val userLangFlow: Flow<String> = preferencesRepository.userLangFlow
    val isOnboardingCompletedFlow: Flow<Boolean> = preferencesRepository.isOnboardingCompletedFlow
    val userIdFlow: Flow<String> = preferencesRepository.userIdFlow

    /**
     * Ensures initial 21 quotes exist in Room without loading them all into memory during app use.
     */
    suspend fun ensureQuotesInitialized() {
        if (quoteDao.countTotalQuotes() == 0) {
            quoteDao.insertAll(InitialQuotes.items)
        }
    }

    /**
     * Lazily loads ONLY the next single unseen quote.
     * Never loads all quotes into memory at startup.
     */
    suspend fun getNextUnseenQuote(lang: String): QuoteEntity? {
        ensureQuotesInitialized()
        return quoteDao.getNextUnseenQuote(lang)
    }

    /**
     * Marks the given quote as seen. It will never be repeated.
     */
    suspend fun markQuoteAsSeen(quoteId: Long) {
        quoteDao.markQuoteAsSeen(quoteId)
    }

    /**
     * Saves user profile to both Room and DataStore.
     */
    suspend fun saveUserProfile(name: String, lang: String) {
        preferencesRepository.saveName(name)
        preferencesRepository.saveLanguage(lang)
        userDao.insertOrUpdate(UserEntity(name = name, lang = lang))
    }

    suspend fun completeOnboarding() {
        preferencesRepository.setOnboardingCompleted(true)
    }

    /**
     * Synchronizes new quotes from private GitHub repository.
     * Reserved for future implementation after initial V1 creation.
     */
    suspend fun syncFromRemote() {
        // Will download new quotes from private GitHub repository in future release.
    }
}
