namespace TFG.Core.Model
{
    public interface ISensorStatusListener
    {
        void OnStatusChanged(Sensor sender, SensorStatus newStatus);
    }
}
