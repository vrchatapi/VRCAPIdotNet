using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using VRCAPIdotNet.VRCAPI.Endpoints;
using VRCAPIdotNet.VRCAPI.Responses;
using VRCAPIdotNet.VRCAPI;
using static VRCAPIdotNet.VRCAPI.Dependencies;
using System.IO;
using System.Diagnostics;

namespace VRCAPIdotNet
{
    public class Core
    {
        public string seperator = "-----\n";
        public static UserSelfRES selfRES = null;
        public static VRCAPIClient client = null;
        public static int index = 0;
        public static List<string> apiMenu;
        public static string selected;

        public static void Main(string[] args)
        {
            SetConsoleUTF8();
            new Core().MainAsync().GetAwaiter().GetResult();
        }

        public static void SetConsoleUTF8()
        {
            Console.WriteLine("Setting console output to UTF-8");
            var cmd = new Process
            {
                StartInfo = {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };
            cmd.Start();

            cmd.StandardInput.WriteLine("chcp 65001");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            Console.OutputEncoding = Encoding.UTF8;
        }

        public async Task MainAsync()
        {
            Console.Title = "VRChatAPIdotNET";

            apiMenu = new List<string>()
            {
                "1. Grab Avatar by ID",
                "2. Grab Avatars",
                "3. Grab World by ID",
                "4. Grab Worlds",
                "5. Grab World Instance",
                "6. Grab World Metadata",
                "7. Grab User by ID",
                "8. Grab Users",
                "9. Grab User by name M1",
                "10. Grab User by name M2",
                "11. Grab Favorite by ID",
                "12. Grab Favorites",
                "13. Post new Favorite",
                "14. Grab Friends",
                "15. Send Friendrequest by ID",
                "16. Accept Friendrequest by ID",
                "17. Delete Friend by ID",
                "18. Grab Moderations",
                "19. Grab Moderations against other Users",
                "20. Grab Moderations against me",
                "21. Grab Notifications",
                "22. Send Invite",
                "23. Send Message",
                "24. Send Votekick M1",
                "25. Send Votekick M2",
                "26. Grab current VRChat Config",
                "27. Register an Account (obsolete)",
                "28. Update Accountinfo",
                "29. Get Discord Names",
                "Exit"
            };

            while (currentUser == null)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("If you are banned, please restart this program\nafter you're done looking at your moderations.\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(seperator);

                string username;string password;
                var loginFile = new FileInfo("login.txt");
                if (loginFile.Exists) {
                    Console.WriteLine($"Reading login info from {loginFile.Name}");
                    var lines = File.ReadAllLines(loginFile.FullName);
                    username = lines[0]; password = lines[1];
                } else {
                    Console.WriteLine("Username:");
                    username = Console.ReadLine();
                    Console.WriteLine();

                    Console.WriteLine("Password:");
                    password = Console.ReadLine();
                    Console.WriteLine();
                }


                client = new VRCAPIClient(username, password);

                Console.WriteLine(seperator);

                selfRES = await client.Auth.Login();

                if (isBanned)
                    (await client.Moderations.Get()).ForEach(x => { Console.WriteLine($"{x.ip}\n{x.reason}\n{x.created}\n{x.expires}\n"); });

                if (inErrorState)
                    await Task.Delay(3000);
            }

            Console.WriteLine(seperator);

            ConfigRES configRES = await client.Config.Get();

            Console.WriteLine(seperator);

            if (!inErrorState)
            {
                Console.WriteLine("Switching to Main Menu..");

                await Task.Delay(2500);

                Console.Clear();

                while (true)
                {
                    Console.Title = "VRChatAPIdotNET - Main Menu";
                    selected = drawMenu(apiMenu);
                    if (selected == apiMenu[0])
                    {
                        await DoGetAvatarByID();
                    }
                    else if (selected == apiMenu[1])
                    {
                        await DoGetAvatars();
                    }
                    else if (selected == apiMenu[2])
                    {
                        await DoGetWorldByID();
                    }
                    else if (selected == apiMenu[13])
                    {
                        await DoGrabFriends();
                    }
                    else if (selected == apiMenu[26])
                    {
                        await DoRegisterAccount();
                    }
                    else if (selected == apiMenu[28])
                    {
                        await DoGetFriendsDiscord();
                    }
                    else if (selected == "Exit")
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        private async Task DoGetFriendsDiscord()
        {
            Console.Clear();
            Console.Title = $"VRChatAPIdotNET - {selected}";
            var regex = new Regex(@".*#(\d{4})", RegexOptions.Multiline);
            (await client.Friends.GetAll()).ForEach(f =>
            {
                if (!string.IsNullOrWhiteSpace(f.statusDescription))
                {
                    foreach (Match m in regex.Matches(input: f.statusDescription))
                    {
                        Console.WriteLine($"{f.displayName}:\t\t\t{m.Value}");
                    }
                }
            });
            Console.Read();
        }

        private async Task DoGrabFriends()
        {
            Console.Clear();
            Console.Title = $"VRChatAPIdotNET - {selected}";

            (await client.Friends.GetAll()).ForEach(f =>
            {
                Console.WriteLine($"Name: {f.displayName}");
                Console.WriteLine($"ID: {f.id}");
                Console.WriteLine($"Offline: {f.location} | {f.worldId}");
                Console.Read();
            });
        }

        private static string drawMenu(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo ckey = Console.ReadKey();

            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == items.Count - 1)
                {
                    index = 0; //Remove the comment to return to the topmost item in the list
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = apiMenu.Count - 1; //Remove the comment to return to the item in the bottom of the list
                }
                else { index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[index];
            }
            else
            {
                return "";
            }

            Console.Clear();
            return "";
        }

        public async Task DoGetAvatarByID()
        {
            Console.Clear();
            Console.Title = $"VRChatAPIdotNET - {selected}";

            Console.WriteLine("Type in the ID of the Avatar:");
            string id = Console.ReadLine();
            Console.WriteLine();
            AvatarRES aRES = await client.Avatars.GetSingle(id);
            foreach (var prop in aRES.GetType().GetProperties())
            {
                if (prop is IEnumerable)
                {
                    Console.WriteLine($"--- {prop.Name} start ---");

                    foreach (object x in (prop as IEnumerable))
                        Console.WriteLine(x.ToString());

                    Console.WriteLine($"--- {prop.Name} end ---");
                }
                else
                    Console.WriteLine($"{prop.Name} :: {prop.GetValue(aRES, null)}");
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the Main Menu.");
            Console.Read();
        }

        public async Task DoGetAvatars()
        {
            Console.Clear();
            Console.Title = $"VRChatAPIdotNET - {selected}";

            Console.WriteLine("Do you want to apply search parameters? y/n");
            if (Console.ReadLine() == "y")
            {
                int n = 0;
                int offs = 0;

                Console.WriteLine("How many avatars should be displayed?");
                string amount = Console.ReadLine();
                if (Int32.TryParse(amount, out n))
                {
                    Console.WriteLine();

                    Console.WriteLine("Enter the offset where the search should begin:");
                    string offset = Console.ReadLine();
                    if (Int32.TryParse(offset, out offs))
                    {
                        Console.WriteLine();

                        List<AvatarRES> aRESList = await client.Avatars.Get(null, null, null, null, n, offs);

                        foreach (AvatarRES aRES in aRESList)
                        {
                            foreach (var prop in aRES.GetType().GetProperties())
                            {
                                if (prop.Name == "tags")
                                {
                                    Console.WriteLine($"--- {prop.Name} start ---");

                                    List<string> newProp = (List<string>)prop.GetValue(aRES, null);
                                    Console.WriteLine(String.Join(",\n", newProp));

                                    Console.WriteLine($"--- {prop.Name} end ---");
                                }
                                else
                                    Console.WriteLine($"{prop.Name} :: {prop.GetValue(aRES, null)}");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
            else if (Console.ReadLine() == "n")
            {
                int n = 0;
                int offs = 0;

                Console.WriteLine("How many avatars should be displayed?");
                string amount = Console.ReadLine();
                if (Int32.TryParse(amount, out n))
                {
                    Console.WriteLine();

                    Console.WriteLine("Enter the offset where the search should begin:");
                    string offset = Console.ReadLine();
                    if (Int32.TryParse(offset, out offs))
                    {
                        Console.WriteLine();

                        List<AvatarRES> aRESList = await client.Avatars.Get(null, false, null, null, n, offs, OrderOptionsA.Descending, null, SortOptionsA.Order);

                        foreach (AvatarRES aRES in aRESList)
                        {
                            foreach (var prop in aRES.GetType().GetProperties())
                            {
                                if (prop.Name == "tags")
                                {
                                    Console.WriteLine($"--- {prop.Name} start ---");

                                    List<string> newProp = (List<string>)prop.GetValue(aRES, null);
                                    Console.WriteLine(String.Join(",\n", newProp));

                                    Console.WriteLine($"--- {prop.Name} end ---");
                                }
                                else
                                    Console.WriteLine($"{prop.Name} :: {prop.GetValue(aRES, null)}");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
            else
                Console.WriteLine("Yikes! Wrong input.");

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the Main Menu.");
            Console.Read();
        }

        public async Task DoGetWorldByID()
        {
            Console.Clear();
            Console.Title = $"VRChatAPIdotNET - {selected}";

            Console.WriteLine("Type in the ID of the World:");
            string id = Console.ReadLine();
            Console.WriteLine();

            WorldSelfRES wSRES = await client.Worlds.GetSingle(id);
            foreach (var prop in wSRES.GetType().GetProperties())
            {
                if (prop.Name == "tags")
                {
                    Console.WriteLine($"--- {prop.Name} start ---");

                    List<string> newProp = (List<string>)prop.GetValue(wSRES, null);
                    Console.WriteLine(String.Join(",\n", newProp));

                    Console.WriteLine($"--- {prop.Name} end ---");
                }
                else if (prop.Name == "unityPackages")
                {
                    Console.WriteLine($"--- {prop.Name} start ---");

                    List<UnityPackage> newProp = (List<UnityPackage>)prop.GetValue(wSRES, null);
                    newProp.ForEach(x => {
                        /*Console.WriteLine(
                            $"\tid: {x.id}\n" +
                            $"\tassetUrl: {x.assetUrl}\n" +
                            $"\tpluginUrl: {x.pluginUrl}\n" +
                            $"\tunityVersion: {x.unityVersion}\n" +
                            $"\tunitySortNumber: {x.unitySortNumber}\n" +
                            $"\tassetVersion: {x.assetVersion}\n" +
                            $"\tplatform: {x.platform}\n" +
                            $"\tcreated_at: {x.createdTime}\n");*/

                        foreach (PropertyInfo propI in x.GetType().GetProperties())
                            Console.WriteLine($"\t{propI.Name}: {propI.GetValue(x, null)}");
                    });

                    Console.WriteLine($"--- {prop.Name} end ---");
                }
                else if (prop.Name == "_instances")
                {
                    Console.WriteLine($"--- {prop.Name} start ---");

                    List<JArray> newProp = (List<JArray>)prop.GetValue(wSRES, null);
                    Console.WriteLine(String.Join(",\n", newProp));

                    Console.WriteLine($"--- {prop.Name} end ---");
                }
                else if (prop.Name == "instances")
                {
                    continue;
                }
                else
                    Console.WriteLine($"{prop.Name} :: {prop.GetValue(wSRES, null)}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the Main Menu.");
            Console.Read();
        }

        public async Task DoRegisterAccount()
        {
            Console.Clear();
            Console.Title = $"VRChatAPIdotNET - {selected}";

            Console.WriteLine("Enter a username:");
            string username = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter a password:");
            string password = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine();
            Console.WriteLine();

            await client.Auth.RegisterV(username, password, email, null, "6");

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the Main Menu.");
            Console.Read();
        }
    }
}