using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace SaveCanvas;

public class PlayerHudTweaks() : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/playerhud.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        MultiTokenWaiter emoteMenu = new([
            t => t is IdentifierToken { Name: "EMOTE"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
        ]);

        foreach (var token in tokens) {
            if (emoteMenu.Check(token)) {
                yield return token;

                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("Input");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_action_just_pressed");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("dev_toggle"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 4);

                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("area");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("player");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("paint_node/Area"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_overlapping_areas");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 5);

                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("area");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("is_in_group");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("canvas"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 6);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("directory");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Directory");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("new");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 6);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("DIR_PATH");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("user://drawings/"));
                yield return new Token(TokenType.Newline, 6);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("path_exists");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 6);

                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("directory");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("dir_exists");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("DIR_PATH");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 7);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("error_code");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("directory");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("make_dir");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("DIR_PATH");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 7);

                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("error_code");
                yield return new Token(TokenType.OpNotEqual);
                yield return new IdentifierToken("OK");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 8);

                yield return new IdentifierToken("path_exists");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline, 6);

                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("path_exists");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 7);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("imageName");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Time");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_datetime_string_from_system");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("replace");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant(":"));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant(" "));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(".png"));
                yield return new Token(TokenType.Newline, 7);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("img");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("area");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_parent");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("Viewport"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_texture");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_data");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 7);

                yield return new IdentifierToken("img");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("convert");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("Image");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("FORMAT_RGBA8");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 7);

                yield return new IdentifierToken("img");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("save_png");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("DIR_PATH");
                yield return new Token(TokenType.OpAdd);
                yield return new IdentifierToken("imageName");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 7);

                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("Saved canvas as "));
                yield return new Token(TokenType.OpAdd);
                yield return new IdentifierToken("imageName");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 6);

                yield return new Token(TokenType.CfBreak);
            } else {
                yield return token;
            }
        }
    }
}