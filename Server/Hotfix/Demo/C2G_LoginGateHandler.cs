﻿using System;
using System.Linq;


namespace ET
{
	[MessageHandler]
	[FriendClass(typeof(SessionPlayerComponent))]
	public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
	{
		protected override async ETTask Run(Session session, C2G_LoginGate request, G2C_LoginGate response, Action reply)
		{
			Scene scene = session.DomainScene();
			string account = scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
			if (account == null)
			{
				response.Error = ErrorCore.ERR_ConnectGateKeyError;
				response.Message = "Gate key验证失败!";
				reply();
				return;
			}


			session.RemoveComponent<SessionAcceptTimeoutComponent>();

			PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();

			foreach (Player item in playerComponent.Children.Values)
			{
				if (item.Account==account)
				{
					session.Send(new G2C_TreeNode() {RootETNode=ETHelper.ToETNode(Game.Scene) });
					response.Error = ErrorCode.ERR_SystemError;
					response.Message = $"已存在玩家{account}";
					reply();
					return;
				}
			}

			Player player = playerComponent.AddChild<Player, string>(account);
            Console.WriteLine($"玩家名字{account}");
			playerComponent.Add(player);
			session.AddComponent<SessionPlayerComponent>().PlayerId = player.Id;
			session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
            var nodes = ETHelper.ToETNode(Game.Scene);
            Console.WriteLine(nodes.ToString());

            response.PlayerId = player.Id;
			reply();
			await ETTask.CompletedTask;
		}
	}
}