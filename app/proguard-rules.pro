# ==============================================================================
# Forge — Proprietary Stoic Discipline Application
# ProGuard / R8 Shrinking, Optimization & Obfuscation Configuration
# ==============================================================================

# Enable aggressive class repackaging and access modification
-repackageclasses 'com.karlitodev.forge.internal'
-allowaccessmodification

# Optimization settings
-optimizationpasses 5
-optimizations !code/simplification/arithmetic,!field/*,!class/merging/*

# Strip line numbers and source file names for maximum intellectual property protection
-renamesourcefileattribute SourceFile
-keepattributes SourceFile,LineNumberTable

# Preserve Android core components and framework lifecycles
-keep public class * extends android.app.Activity
-keep public class * extends android.app.Application
-keep public class * extends android.app.Service
-keep public class * extends android.content.BroadcastReceiver
-keep public class * extends android.content.ContentProvider
-keep public class * extends android.app.backup.BackupAgentHelper
-keep public class * extends android.preference.Preference
-keep public class * extends androidx.fragment.app.Fragment

# Keep custom views / controls referenced from XML or reflections
-keepclasseswithmembers class * {
    public <init>(android.content.Context, android.util.AttributeSet);
    public <init>(android.content.Context, android.util.AttributeSet, int);
}

# Keep Google Play Billing Client and in-app purchase interfaces
-keep class com.android.vending.billing.** { *; }
-keep class com.android.billingclient.api.** { *; }
-keep interface com.android.billingclient.api.** { *; }

# Keep Room database schemas and entities
-keep class androidx.room.** { *; }
-keep class * extends androidx.room.RoomDatabase
-dontwarn androidx.room.paging.**

# Keep Kotlin reflect and coroutines metadata
-keepattributes *Annotation*,Signature,InnerClasses,EnclosingMethod
-keepclassmembers class * {
    @androidx.room.* *;
}

# Preserve parcelable and serializable members
-keepclassmembers class * implements android.os.Parcelable {
    static ** CREATOR;
}
-keepclassmembers enum * {
    public static **[] values();
    public static ** valueOf(java.lang.String);
}

# Strip all debug logging from release binaries
-assumenosideeffects class android.util.Log {
    public static boolean isLoggable(java.lang.String, int);
    public static int v(...);
    public static int d(...);
    public static int i(...);
}
