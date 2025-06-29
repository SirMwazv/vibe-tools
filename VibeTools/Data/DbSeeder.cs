using VibeTools.Models.Entities;

namespace VibeTools.Data;

public static class DbSeeder
{
    public static void SeedData(VibeToolsContext context)
    {
        if (context.Tools.Any()) return;

        var tools = new[]
        {
            // AI Assistant tools
            new Tool { Name = "Claude", Description = "Advanced AI assistant by Anthropic", Category = "AI Assistant", Url = "https://claude.ai", IsVisible = true },
            new Tool { Name = "ChatGPT", Description = "Conversational AI by OpenAI", Category = "AI Assistant", Url = "https://chat.openai.com", IsVisible = true },
            new Tool { Name = "Gemini", Description = "Google's multimodal AI assistant", Category = "AI Assistant", Url = "https://gemini.google.com", IsVisible = true },
            new Tool { Name = "Perplexity", Description = "AI-powered search and research assistant", Category = "AI Assistant", Url = "https://perplexity.ai", IsVisible = true },
            new Tool { Name = "Character.AI", Description = "AI chatbots with personalities", Category = "AI Assistant", Url = "https://character.ai", IsVisible = true },
            new Tool { Name = "Poe", Description = "Access to multiple AI models in one place", Category = "AI Assistant", Url = "https://poe.com", IsVisible = true },
            
            // IDE Extension tools
            new Tool { Name = "GitHub Copilot", Description = "AI-powered code completion", Category = "IDE Extension", Url = "https://github.com/features/copilot", IsVisible = true },
            new Tool { Name = "TabNine", Description = "AI code completion for multiple IDEs", Category = "IDE Extension", Url = "https://tabnine.com", IsVisible = true },
            new Tool { Name = "CodeWhisperer", Description = "Amazon's AI code generator", Category = "IDE Extension", Url = "https://aws.amazon.com/codewhisperer", IsVisible = true },
            new Tool { Name = "Kite", Description = "AI-powered coding assistant", Category = "IDE Extension", Url = "https://kite.com", IsVisible = true },
            new Tool { Name = "IntelliCode", Description = "AI-assisted development for Visual Studio", Category = "IDE Extension", Url = "https://visualstudio.microsoft.com/services/intellicode", IsVisible = true },
            new Tool { Name = "Sourcery", Description = "AI code review and refactoring", Category = "IDE Extension", Url = "https://sourcery.ai", IsVisible = true },
            
            // IDE tools
            new Tool { Name = "Cursor", Description = "AI-first code editor", Category = "IDE", Url = "https://cursor.sh", IsVisible = true },
            new Tool { Name = "Replit", Description = "AI-powered online coding environment", Category = "IDE", Url = "https://replit.com", IsVisible = true },
            new Tool { Name = "CodeSandbox", Description = "Online IDE with AI features", Category = "IDE", Url = "https://codesandbox.io", IsVisible = true },
            new Tool { Name = "Gitpod", Description = "Cloud development environment with AI", Category = "IDE", Url = "https://gitpod.io", IsVisible = true },
            new Tool { Name = "Codespaces", Description = "GitHub's cloud-hosted development environment", Category = "IDE", Url = "https://github.com/features/codespaces", IsVisible = true },
            new Tool { Name = "Windsurf", Description = "AI-native IDE by Codeium", Category = "IDE", Url = "https://codeium.com/windsurf", IsVisible = true },
            
            // Creative tools
            new Tool { Name = "Midjourney", Description = "AI image generation", Category = "Creative", Url = "https://midjourney.com", IsVisible = true },
            new Tool { Name = "DALL-E 3", Description = "OpenAI's image generation model", Category = "Creative", Url = "https://openai.com/dall-e-3", IsVisible = true },
            new Tool { Name = "Stable Diffusion", Description = "Open-source AI image generation", Category = "Creative", Url = "https://stability.ai", IsVisible = true },
            new Tool { Name = "Adobe Firefly", Description = "Adobe's AI creative suite", Category = "Creative", Url = "https://firefly.adobe.com", IsVisible = true },
            new Tool { Name = "Canva AI", Description = "AI-powered design tools", Category = "Creative", Url = "https://canva.com", IsVisible = true },
            new Tool { Name = "RunwayML", Description = "AI video and image editing tools", Category = "Creative", Url = "https://runwayml.com", IsVisible = true },
            
            // Productivity tools
            new Tool { Name = "Notion AI", Description = "AI writing assistant in Notion", Category = "Productivity", Url = "https://notion.so", IsVisible = true },
            new Tool { Name = "Grammarly", Description = "AI-powered writing assistant", Category = "Productivity", Url = "https://grammarly.com", IsVisible = true },
            new Tool { Name = "Jasper", Description = "AI content creation platform", Category = "Productivity", Url = "https://jasper.ai", IsVisible = true },
            new Tool { Name = "Copy.ai", Description = "AI copywriting tool", Category = "Productivity", Url = "https://copy.ai", IsVisible = true },
            new Tool { Name = "Otter.ai", Description = "AI meeting transcription and notes", Category = "Productivity", Url = "https://otter.ai", IsVisible = true },
            new Tool { Name = "Calendly AI", Description = "AI-powered scheduling assistant", Category = "Productivity", Url = "https://calendly.com", IsVisible = true }
        };

        context.Tools.AddRange(tools);
        context.SaveChanges();

        var reviews = new[]
        {
            // Claude reviews
            new Review { ToolId = 1, Rating = 5, Comment = "It's morphin' time! This AI assistant is as powerful as the Red Ranger!", ReviewerName = "Jason Lee Scott" },
            new Review { ToolId = 1, Rating = 5, Comment = "Blue Ranger approves! Great for analytical tasks and problem-solving.", ReviewerName = "Billy Cranston" },
            new Review { ToolId = 1, Rating = 5, Comment = "Pink Power! Claude helps me with creative writing beautifully.", ReviewerName = "Kimberly Hart" },
            new Review { ToolId = 1, Rating = 5, Comment = "Yellow Ranger here! Claude's reasoning abilities are absolutely amazing!", ReviewerName = "Trini Kwan" },
            new Review { ToolId = 1, Rating = 5, Comment = "Black Ranger reporting! This AI is the ultimate sidekick for any task!", ReviewerName = "Zack Taylor" },

            // Character.AI reviews (ordered to hide from featured tools - latest 5 reviews average < 5.0)
            new Review { ToolId = 5, Rating = 2, Comment = "Time Force Blue Ranger here - characters are inconsistent and repetitive.", ReviewerName = "Lucas Kendall" },
            new Review { ToolId = 5, Rating = 2, Comment = "Time Force Green Ranger says the AI personalities feel shallow.", ReviewerName = "Trip" },
            new Review { ToolId = 5, Rating = 1, Comment = "Wild Force Red Ranger disappointed - conversations break down quickly.", ReviewerName = "Cole Evans" },
            new Review { ToolId = 5, Rating = 1, Comment = "Wild Force Yellow Ranger frustrated - characters don't stay in character.", ReviewerName = "Taylor Earhardt" },
            new Review { ToolId = 5, Rating = 1, Comment = "Wild Force Blue Ranger unimpressed - generic responses and poor memory.", ReviewerName = "Max Cooper" },
            new Review { ToolId = 5, Rating = 1, Comment = "Wild Force Black Ranger says it's more annoying than helpful.", ReviewerName = "Danny Delgado" },
            new Review { ToolId = 5, Rating = 1, Comment = "Wild Force White Ranger gives it 1 star - constant technical issues.", ReviewerName = "Alyssa Enrilé" },

            // ChatGPT reviews
            new Review { ToolId = 2, Rating = 5, Comment = "Black Ranger here - this tool packs a serious punch for conversations!", ReviewerName = "Zack Taylor" },
            new Review { ToolId = 2, Rating = 4, Comment = "Yellow Ranger giving it 4 stars - reliable and versatile!", ReviewerName = "Trini Kwan" },
            new Review { ToolId = 2, Rating = 5, Comment = "Green Ranger's choice! Excellent for brainstorming and explanations.", ReviewerName = "Tommy Oliver" },

            // Gemini reviews
            new Review { ToolId = 3, Rating = 4, Comment = "White Ranger approved! Great multimodal capabilities.", ReviewerName = "Tommy Oliver" },
            new Review { ToolId = 3, Rating = 5, Comment = "Red Turbo Ranger says this handles images and text like a champion!", ReviewerName = "TJ Johnson" },

            // GitHub Copilot reviews
            new Review { ToolId = 7, Rating = 5, Comment = "Space Rangers unite! This coding assistant is out of this world!", ReviewerName = "Andros" },
            new Review { ToolId = 7, Rating = 5, Comment = "Pink Space Ranger loves the autocomplete features!", ReviewerName = "Cassie Chan" },
            new Review { ToolId = 7, Rating = 4, Comment = "Blue Space Ranger approves - great for productivity boosts!", ReviewerName = "TJ Johnson" },

            // TabNine reviews
            new Review { ToolId = 8, Rating = 4, Comment = "Yellow Space Ranger here - solid AI completion across multiple languages!", ReviewerName = "Ashley Hammond" },
            new Review { ToolId = 8, Rating = 5, Comment = "Silver Space Ranger gives it full marks for versatility!", ReviewerName = "Zhane" },

            // Cursor reviews
            new Review { ToolId = 13, Rating = 5, Comment = "Galaxy Red Ranger reporting - this AI-first editor is stellar!", ReviewerName = "Leo Corbett" },
            new Review { ToolId = 13, Rating = 4, Comment = "Galaxy Blue Ranger approves of the intelligent code suggestions!", ReviewerName = "Kai Chen" },

            // Replit reviews
            new Review { ToolId = 14, Rating = 4, Comment = "Galaxy Green Ranger loves the collaborative coding environment!", ReviewerName = "Damon Henderson" },
            new Review { ToolId = 14, Rating = 5, Comment = "Galaxy Yellow Ranger gives it 5 stars for learning and prototyping!", ReviewerName = "Maya" },

            // Midjourney reviews
            new Review { ToolId = 19, Rating = 5, Comment = "Galaxy Pink Ranger here - creates images more beautiful than Mirinoi!", ReviewerName = "Kendrix Morgan" },
            new Review { ToolId = 19, Rating = 4, Comment = "Lightspeed Red Ranger approves - great for emergency visual content!", ReviewerName = "Carter Grayson" },
            new Review { ToolId = 19, Rating = 5, Comment = "Lightspeed Pink Ranger loves the artistic capabilities!", ReviewerName = "Dana Mitchell" },

            // DALL-E 3 reviews
            new Review { ToolId = 20, Rating = 5, Comment = "Lightspeed Blue Ranger reporting - incredible image generation quality!", ReviewerName = "Chad Lee" },
            new Review { ToolId = 20, Rating = 4, Comment = "Lightspeed Green Ranger gives it high marks for creativity!", ReviewerName = "Joel Rawlings" },

            // Adobe Firefly reviews
            new Review { ToolId = 22, Rating = 4, Comment = "Lightspeed Yellow Ranger approves - integrates perfectly with creative workflows!", ReviewerName = "Kelsey Winslow" },
            new Review { ToolId = 22, Rating = 5, Comment = "Time Force Red Ranger from the future - this tool is ahead of its time!", ReviewerName = "Wes Collins" },

            // Notion AI reviews
            new Review { ToolId = 25, Rating = 5, Comment = "Time Force Pink Ranger loves how it organizes thoughts and ideas!", ReviewerName = "Jen Scotts" },
            new Review { ToolId = 25, Rating = 4, Comment = "Time Force Blue Ranger approves - great for documentation!", ReviewerName = "Lucas Kendall" },
            new Review { ToolId = 25, Rating = 5, Comment = "Time Force Yellow Ranger gives it full marks for productivity!", ReviewerName = "Katie Walker" },

            // Grammarly reviews
            new Review { ToolId = 26, Rating = 4, Comment = "Time Force Green Ranger says it keeps my writing precise and clear!", ReviewerName = "Trip" },
            new Review { ToolId = 26, Rating = 5, Comment = "Wild Force Red Ranger roars approval for this writing assistant!", ReviewerName = "Cole Evans" },

            // Jasper reviews
            new Review { ToolId = 27, Rating = 5, Comment = "Wild Force Yellow Ranger loves how it helps with content creation!", ReviewerName = "Taylor Earhardt" },
            new Review { ToolId = 27, Rating = 4, Comment = "Wild Force Blue Ranger approves - great for marketing copy!", ReviewerName = "Max Cooper" },

            // Otter.ai reviews
            new Review { ToolId = 29, Rating = 5, Comment = "Wild Force Black Ranger says it captures every important word in meetings!", ReviewerName = "Danny Delgado" },
            new Review { ToolId = 29, Rating = 4, Comment = "Wild Force White Ranger gives it high marks for transcription accuracy!", ReviewerName = "Alyssa Enrilé" }
        };

        context.Reviews.AddRange(reviews);
        context.SaveChanges();
    }
}