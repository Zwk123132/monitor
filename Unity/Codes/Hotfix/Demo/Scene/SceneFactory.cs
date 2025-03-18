namespace ET
{
    public static class SceneFactory
    {
        public static Scene CreateZoneScene(int zone, string name, Entity parent)
        {
            Log.Debug("创建zoneScene时创建了各种组件");
            Scene zoneScene = EntitySceneFactory.CreateScene(Game.IdGenerater.GenerateInstanceId(), zone, SceneType.Zone, name, parent);
            zoneScene.AddComponent<ZoneSceneFlagComponent>();
            zoneScene.AddComponent<NetKcpComponent, int>(SessionStreamDispatcherType.SessionStreamDispatcherClientOuter);
            zoneScene.AddComponent<CurrentScenesComponent>();
            zoneScene.AddComponent<ObjectWait>();
            zoneScene.AddComponent<PlayerComponent>();
            Log.Debug("创建HardwareComponen组件");
            zoneScene.AddComponent<HardwareComponent>();
            //发送ZoneSscene创建完成消息
            Game.EventSystem.Publish(new EventType.AfterCreateZoneScene() {ZoneScene = zoneScene});
            return zoneScene;
        }
        
        public static Scene CreateCurrentScene(long id, int zone, string name, CurrentScenesComponent currentScenesComponent)
        {
            Scene currentScene = EntitySceneFactory.CreateScene(id, IdGenerater.Instance.GenerateInstanceId(), zone, SceneType.Current, name, currentScenesComponent);
            currentScenesComponent.Scene = currentScene;
            
            Game.EventSystem.Publish(new EventType.AfterCreateCurrentScene() {CurrentScene = currentScene});
            return currentScene;
        }
        
        
    }
}