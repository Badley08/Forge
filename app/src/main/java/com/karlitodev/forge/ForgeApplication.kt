package com.karlitodev.forge

import android.app.Application
import com.karlitodev.forge.data.billing.BillingManager
import com.karlitodev.forge.data.local.ForgeDatabase
import com.karlitodev.forge.data.preferences.UserPreferencesRepository
import com.karlitodev.forge.data.repository.ForgeRepository

class ForgeApplication : Application() {

    lateinit var repository: ForgeRepository
        private set

    lateinit var billingManager: BillingManager
        private set

    lateinit var preferencesRepository: UserPreferencesRepository
        private set

    override fun onCreate() {
        super.onCreate()

        val database = ForgeDatabase.getInstance(this)
        preferencesRepository = UserPreferencesRepository(this)
        billingManager = BillingManager(this)

        repository = ForgeRepository(
            database = database,
            preferencesRepository = preferencesRepository,
            billingManager = billingManager
        )
    }

    override fun onTerminate() {
        super.onTerminate()
        billingManager.destroy()
    }
}
