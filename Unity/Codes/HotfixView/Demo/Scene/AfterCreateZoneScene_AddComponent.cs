namespace ET
{
    public class AfterCreateZoneScene_AddComponent: AEvent<EventType.AfterCreateZoneScene>
    {
        protected override void Run(EventType.AfterCreateZoneScene args)
        {
            Scene zoneScene = args.ZoneScene;

            zoneScene.AddComponent<ResourcesLoaderComponent>();


            zoneScene.AddComponent<UIManagerComponent>();
               
        }
    }
}