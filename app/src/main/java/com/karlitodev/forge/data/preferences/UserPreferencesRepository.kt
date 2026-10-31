package com.karlitodev.forge.data.preferences

import android.content.Context
import androidx.datastore.core.DataStore
import androidx.datastore.preferences.core.Preferences
import androidx.datastore.preferences.core.booleanPreferencesKey
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.core.stringPreferencesKey
import androidx.datastore.preferences.preferencesDataStore
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import java.util.UUID

private val Context.dataStore: DataStore<Preferences> by preferencesDataStore(name = "forge_user_prefs")

class UserPreferencesRepository(private val context: Context) {

    private object PreferencesKeys {
        val USER_NAME = stringPreferencesKey("user_name")
        val USER_LANG = stringPreferencesKey("user_lang")
        val ONBOARDING_COMPLETED = booleanPreferencesKey("onboarding_completed")
        val USER_ID = stringPreferencesKey("user_id")
    }

    val userNameFlow: Flow<String> = context.dataStore.data.map { preferences ->
        preferences[PreferencesKeys.USER_NAME] ?: ""
    }

    val userLangFlow: Flow<String> = context.dataStore.data.map { preferences ->
        preferences[PreferencesKeys.USER_LANG] ?: "FR"
    }

    val isOnboardingCompletedFlow: Flow<Boolean> = context.dataStore.data.map { preferences ->
        preferences[PreferencesKeys.ONBOARDING_COMPLETED] ?: false
    }

    val userIdFlow: Flow<String> = context.dataStore.data.map { preferences ->
        preferences[PreferencesKeys.USER_ID] ?: ""
    }

    suspend fun saveName(name: String) {
        context.dataStore.edit { preferences ->
            preferences[PreferencesKeys.USER_NAME] = name.trim()
            if (preferences[PreferencesKeys.USER_ID].isNullOrEmpty()) {
                preferences[PreferencesKeys.USER_ID] = UUID.randomUUID().toString()
            }
        }
    }

    suspend fun saveLanguage(lang: String) {
        context.dataStore.edit { preferences ->
            preferences[PreferencesKeys.USER_LANG] = lang.uppercase()
        }
    }

    suspend fun setOnboardingCompleted(completed: Boolean = true) {
        context.dataStore.edit { preferences ->
            preferences[PreferencesKeys.ONBOARDING_COMPLETED] = completed
            if (preferences[PreferencesKeys.USER_ID].isNullOrEmpty()) {
                preferences[PreferencesKeys.USER_ID] = UUID.randomUUID().toString()
            }
        }
    }
}
