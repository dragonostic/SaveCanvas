using GDWeave;

namespace SaveCanvas;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.Logger.Information("Hello, world!");
        modInterface.RegisterScriptMod(new PlayerHudTweaks());
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
