namespace DevCameraMod
{
    public static class Extensions
    {
        public static VRRig GetRigByNetPlayer(this NetPlayer netPlayer) =>
                GorillaParent.instance.vrrigs.Find(r => r.OwningNetPlayer == netPlayer);
    }
}