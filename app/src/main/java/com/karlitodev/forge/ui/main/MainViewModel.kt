package com.karlitodev.forge.ui.main

import android.app.Activity
import android.content.Context
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.karlitodev.forge.data.local.QuoteEntity
import com.karlitodev.forge.data.repository.ForgeRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch

class MainViewModel(
    private val repository: ForgeRepository
) : ViewModel() {

    private val _currentQuote = MutableStateFlow<QuoteEntity?>(null)
    val currentQuote: StateFlow<QuoteEntity?> = _currentQuote.asStateFlow()

    private val _isAllRead = MutableStateFlow(false)
    val isAllRead: StateFlow<Boolean> = _isAllRead.asStateFlow()

    private val _isLoading = MutableStateFlow(true)
    val isLoading: StateFlow<Boolean> = _isLoading.asStateFlow()

    val isPremium: StateFlow<Boolean> = repository.isPremiumFlow
        .stateIn(viewModelScope, SharingStarted.Eagerly, false)

    val userName: StateFlow<String> = repository.userNameFlow
        .stateIn(viewModelScope, SharingStarted.Lazily, "")

    val userId: StateFlow<String> = repository.userIdFlow
        .stateIn(viewModelScope, SharingStarted.Lazily, "")

    init {
        loadNextQuote()
    }

    fun loadNextQuote() {
        viewModelScope.launch {
            _isLoading.value = true
            val lang = repository.userLangFlow.first().ifBlank { "FR" }
            val next = repository.getNextUnseenQuote(lang)
            if (next != null) {
                _currentQuote.value = next
                _isAllRead.value = false
            } else {
                _currentQuote.value = null
                _isAllRead.value = true
            }
            _isLoading.value = false
        }
    }

    fun onNextQuoteClicked() {
        val quote = _currentQuote.value ?: return
        viewModelScope.launch {
            // Mark current quote as seen in Room. It will never repeat.
            repository.markQuoteAsSeen(quote.id)
            loadNextQuote()
        }
    }

    fun onShareClicked(context: Context, activity: Activity?) {
        val quote = _currentQuote.value ?: return
        if (isPremium.value) {
            WatermarkedQuoteSharing.shareWatermarkedQuote(
                context = context,
                quote = quote,
                userId = userId.value
            )
        } else if (activity != null) {
            repository.billingManager.launchBillingFlow(activity)
        }
    }

    fun launchPremiumUpgrade(activity: Activity) {
        repository.billingManager.launchBillingFlow(activity)
    }
}
