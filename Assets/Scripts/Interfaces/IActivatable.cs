public interface IActivatable {
    void ActivateAction(int actionID);
}

//Designed for drag and drop action assignment, assuming it already has an action coded
//Script attached to buttons or other switches activatables has refence for IActivatable
//The IActivatable will perform an action once activated
//actionID is for the case of multible buttons sending mesages to the same object
//Could do event, but with how many there could be, it would be annoying to manage
//Can be chained, player activated button which activate door
