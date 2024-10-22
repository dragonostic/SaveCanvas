# GDWeave.Sample

A sample for C# GDWeave mods.

## Setup

Clone/fork/whatever this repository and rename the following:

- Solution and project file name
- Project namespace
- `Id` field in `manifest.json`

To build the project, you need to set the `GDWeavePath` environment variable to your game install's GDWeave directory (e.g. `G:\games\steam\steamapps\common\WEBFISHING\GDWeave`).

## Writing script mods

You can register a script mod through the mod interface:

```cs
modInterface.RegisterScriptMod(new MyScriptMod());
```

For example, if you wanted to wait until the newline after the `extends` keyword:

```cs
public class MyScriptMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://path/to/script.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline token after any extends token
        var waiter = new MultiTokenWaiter([
            t => t.Type is TokenType.PrExtends,
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                // found our match, return the original newline
                yield return token;

                // then add our own code
                yield return new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.TextPrint);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("Hello, world!"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline);
            } else {
                // return the original token
                yield return token;
            }
        }
    }
}
```

You can use the `TokenWaiter`/`MultiTokenWaiter`/`TokenConsumer` helper classes to wait until a specific token appears. Scripts run top-to-bottom, operating on each token of the script. `yield return` adds a new token to the result, so in this example we return all tokens but make sure to add our own code right below the newline after the `extends` keyword.

If you are unfamiliar with C# enumerators, consider [reading the documentation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield) for how it works.
