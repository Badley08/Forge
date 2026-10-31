# ==============================================================================
# Forge Avalonia — R8 Shrinking, Optimization & Obfuscation
# ==============================================================================

-repackageclasses 'com.karlitodev.forge.internal'
-allowaccessmodification
-optimizationpasses 5

# Strip line numbers and source file names for maximum intellectual property protection
-renamesourcefileattribute SourceFile
-keepattributes SourceFile,LineNumberTable

# Preserve Avalonia Android runtime & lifecycle
-keep class avalonia.** { *; }
-keep class Avalonia.** { *; }
-keep class * extends avalonia.android.** { *; }

# Preserve Google Play Billing and Plugin.InAppBilling
-keep class com.android.vending.billing.** { *; }
-keep class com.android.billingclient.api.** { *; }
-keep class plugin.inappbilling.** { *; }

# Preserve Android application and activity entry points
-keep public class * extends android.app.Activity
-keep public class * extends android.app.Application

# Strip debug logs
-assumenosideeffects class android.util.Log {
    public static boolean isLoggable(java.lang.String, int);
    public static int v(...);
    public static int d(...);
    public static int i(...);
}
