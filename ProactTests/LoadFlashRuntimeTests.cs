using Proact;

namespace ProactTests;

public class LoadFlashRuntimeTests
{
    [Fact]
    public void Verify_that_the_flash_runtime_script_is_imported()
    {
        var flashRuntimeJs = LoadFlashRuntime.FlashJavascriptRuntime;
        Assert.NotNull(flashRuntimeJs);
    }
}