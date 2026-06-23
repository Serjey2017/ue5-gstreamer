using UnrealBuildTool;
using System.IO;

public class GStreamer : ModuleRules
{
    public GStreamer(ReadOnlyTargetRules Target) : base(Target)
    {
        DefaultBuildSettings = BuildSettingsVersion.V5;
        PCHUsage = PCHUsageMode.NoPCHs;
        bUseUnity = false;
        CppCompileWarningSettings.UndefinedIdentifierWarningLevel = WarningLevel.Off;

        PublicDependencyModuleNames.AddRange(
            new string[] {
                "Core",
                "CoreUObject",
                "Engine",
                "InputCore",
                "RHI",
                "RenderCore",
                "Slate",
                "SlateCore"
            }
        );

        if (Target.Platform == UnrealTargetPlatform.Win64)
        {
            string GStreamerRoot =
                System.Environment.GetEnvironmentVariable("GSTREAMER_1_0_ROOT_MSVC_X86_64") ??
                System.Environment.GetEnvironmentVariable("GSTREAMER_ROOT_X86_64") ??
                System.Environment.GetEnvironmentVariable("GSTREAMER_ROOT");

            if (string.IsNullOrEmpty(GStreamerRoot))
                throw new System.Exception("GStreamer not found. Install GStreamer and set GSTREAMER_1_0_ROOT_MSVC_X86_64.");

            GStreamerRoot = GStreamerRoot.TrimEnd('\\', '/');

            PrivateIncludePaths.Add(Path.Combine(GStreamerRoot, "include"));
            PrivateIncludePaths.Add(Path.Combine(GStreamerRoot, "include", "gstreamer-1.0"));
            PrivateIncludePaths.Add(Path.Combine(GStreamerRoot, "include", "glib-2.0"));
            PrivateIncludePaths.Add(Path.Combine(GStreamerRoot, "lib", "glib-2.0", "include"));

            var GStreamerLibPath = Path.Combine(GStreamerRoot, "lib");
            PublicSystemLibraryPaths.Add(GStreamerLibPath);

            PublicAdditionalLibraries.Add(Path.Combine(GStreamerLibPath, "glib-2.0.lib"));
            PublicAdditionalLibraries.Add(Path.Combine(GStreamerLibPath, "gobject-2.0.lib"));
            PublicAdditionalLibraries.Add(Path.Combine(GStreamerLibPath, "gstreamer-1.0.lib"));
            PublicAdditionalLibraries.Add(Path.Combine(GStreamerLibPath, "gstvideo-1.0.lib"));
            PublicAdditionalLibraries.Add(Path.Combine(GStreamerLibPath, "gstapp-1.0.lib"));

            PublicDelayLoadDLLs.Add("glib-2.0-0.dll");
            PublicDelayLoadDLLs.Add("gobject-2.0-0.dll");
            PublicDelayLoadDLLs.Add("gstreamer-1.0-0.dll");
            PublicDelayLoadDLLs.Add("gstvideo-1.0-0.dll");
            PublicDelayLoadDLLs.Add("gstapp-1.0-0.dll");
        }
    }
}
