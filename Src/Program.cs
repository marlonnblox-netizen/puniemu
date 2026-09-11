using Microsoft.AspNetCore.Rewrite;
using Puniemu.Src.Server.GameServer.Requests.DefaultHandler.Logic;
using Puniemu.Src.Server.GameServer.Requests.GetL5IDStatus.Logic;
using Puniemu.Src.Server.GameServer.Requests.CreateUser.Logic;
using Puniemu.Src.Server.GameServer.Requests.Init.Logic;
using Puniemu.Src.Server.GameServer.Requests.GetMaster.Logic;
using Puniemu.Src.Server.L5ID.Requests.CreateGDKey.Logic;
using Puniemu.Src.Server.GameServer.Requests.GetGdkeyAccounts.Logic;
using Puniemu.Src.Server.GameServer.Requests.UpdateProfile.Logic;
using Puniemu.Src.Server.GameServer.Requests.DeleteUser.Logic;
using Puniemu.Src.Server.GameServer.Requests.UserInfoRefresh.Logic;
using Puniemu.Src.Server.GameServer.Requests.UserStageRanking.Logic;
using Puniemu.Src.Server.GameServer.Requests.Login.Logic;
using Puniemu.Src.Server.GameServer.Requests.BuyHitodama.Logic;
using Puniemu.Src.Server.GameServer.Requests.InitBilling.Logic;
using Puniemu.Src.Server.GameServer.Requests.DeckEdit.Logic;
using Puniemu.Src.Server.GameServer.Requests.GameEnd.Logic;
using Puniemu.Src.Server.GameServer.Requests.GameEnd;
using Puniemu.Src.Server.GameServer.Requests.Rename.Logic;
using Puniemu.Src.Server.GameServer.Requests.GameUseItem.Logic;
using Puniemu.Src.Server.GameServer.Requests.GameContinue.Logic;
using Puniemu.Src.Server.GameServer.Requests.LoginStamp.Logic;
using Puniemu.Src.Server.GameServer.Requests.ExecuteGacha.Logic;
using Puniemu.Src.Server.GameServer.Requests.InitCollectMenu.Logic;
using Puniemu.Src.Server.GameServer.Requests.Friend.Logic;
using Puniemu.Src.Server.GameServer.Requests.FriendRequest.Logic;
using Puniemu.Src.Server.GameServer.Requests.FriendSearch.Logic;
using Puniemu.Src.Server.GameServer.Requests.FriendRequestDelete.Logic;
using Puniemu.Src.Server.GameServer.Requests.InitGoku.Logic;
using Puniemu.Src.Server.GameServer.Requests.UpdateGokuStory.Logic;
using Puniemu.Src.Server.GameServer.Requests.UpdateGokuMenu.Logic;
using Puniemu.Src.Server.GameServer.Requests.InitCrystal.Logic;
using Puniemu.Src.Server.GameServer.Requests.UpdateCrystalMenu.Logic;
using Puniemu.Src.Server.GameServer.Requests.FriendDelete.Logic;
using Puniemu.Src.Server.GameServer.Requests.GetPresentBox.Logic;
using Puniemu.Src.Server.GameServer.Requests.GameEndScoreAttack.Logic;
using Puniemu.Src.Server.GameServer.Requests.InitScoreAttack.Logic;
using Puniemu.Src.Server.GameServer.Requests.StartScoreAttack.Logic;
using Puniemu.Src.Server.GameServer.Requests.MapWarp.Logic;
using Puniemu.Src.Server.GameServer.Requests.MapUnLock.Logic;
using Puniemu.Src.Server.GameServer.Requests.LaunchingInfos.Logic;


using Puniemu.Src.Utils.GeneralUtils;
using Puniemu.Src.Server.GameServer.Requests.FriendRequestAccept.Logic;
using Puniemu.Src.Server.GameServer.Requests.GetRanking.Logic;
using Puniemu.Src.Server.L5ID.Requests;
using Puniemu.Src.DataManager.Logic;
using Puniemu.Src.Server.GameServer.Requests.UpdateTutorialFlag.Logic;
using Puniemu.Src.Server.GameServer.Requests.Game.GameStart.Logic;
using Newtonsoft.Json;
using Puniemu.Src.Server.GameServer.Requests.Init.InitGacha.Logic.Puni;
using Puniemu.Src.Server.GameServer.Requests.Init.InitGacha.Logic.WibWob;
using Puniemu.Src.Server.GameServer.Requests.BuyItem.Logic;
using Puniemu.Src.Server.GameServer.Requests.Map.Map.Logic;
using Puniemu.Src.Server.GameServer.Requests.LevelLockOff.Logic;
using Puniemu.Src.Server.GameServer.Requests.GetMission.Logic;
using Puniemu.Src.Server.GameServer.Requests.UseItem.Logic;
using Puniemu.Src.Server.GameServer.Requests.Watch.InitWatch.Logic;
using Puniemu.Src.Server.GameServer.Requests.Watch.UpdateWatchReadFlg.Logic;
using Puniemu.Src.Server.GameServer.Requests.Conflate.Logic;
using Puniemu.Src.Server.GameServer.Requests.UseAddition.Logic;
using Puniemu.Src.Server.GameServer.Requests.MissionReward.Logic;
using Puniemu.Src.Server.CustomAuth.Requests.Link.Logic;
using Puniemu.Src.Server.GameServer.Requests.SerialConfirm.Logic;
using Puniemu.Src.Server.GameServer.Requests.ReleaseYoukai.Logic;
using Puniemu.Src.Server.GameServer.Requests.EvolveYoukai.Logic;
namespace Puniemu.Src;
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Add the config to DataManager so it can be used globally
        DataManager.Logic.DataManager.StaticInit(builder.Configuration);

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Limits.MaxConcurrentConnections = DataManager.Logic.DataManager.MaxConnections;
        });

        var app = builder.Build();
        //Rewrite to redirect mainly all .NHN requests to .NHN/, as ASP.NET Core thinks it's static serving otherwise or something 
        //second rewrite is in case it's for example /////////////////////init.nhn it makes it /init.nhn
        var rewriteOptions = new RewriteOptions()
           .AddRewrite(@"^/+(.+)$", "/$1", skipRemainingRules: false)
           .AddRewrite(@"^(.+\.nhn)$", "$1/", skipRemainingRules: false);


        app.UseRewriter(rewriteOptions);

        //Refuse new players while the account cache is at capacity
        app.
