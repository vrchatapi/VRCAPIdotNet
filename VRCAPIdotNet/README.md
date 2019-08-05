# VRCAPIdotNet

> Almost all credits go to Native mah boi


Example Client:
```csharp
using VRCAPIdotNet.VRCAPI;
using VRCAPIdotNet.VRCAPI.Endpoints;
using VRCAPIdotNet.VRCAPI.Responses;
using static VRCAPIdotNet.VRCAPI.Dependencies;
namespace myVRCAPIdotNetClient

    public class Client
    {
        public static UserSelfRES selfRES = null;
        public static VRCAPIClient client = null;

        public static void Main(string[] args)
        {
            new Client().MainAsync().GetAwaiter().GetResult();
        }
        public async Task MainAsync()
        {
            while (currentUser == null)
            {

                client = new VRCAPIClient(username, password);
                selfRES = await client.Auth.Login();

                if (isBanned) {
					/* Do something */
				}

                if (inErrorState) {
                    await Task.Delay(3000);
				}
            }
            ConfigRES configRES = await client.Config.Get();

            if (!inErrorState) {
				(await client.Friends.GetAll()).ForEach(f =>
				{
					Console.WriteLine($"Name: {f.displayName}");
					Console.WriteLine($"ID: {f.id}");
					Console.WriteLine($"Offline: {f.location} | {f.worldId}");
					Console.Read();
				});
			}
		}
	}
}
```