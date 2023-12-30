using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public interface IActivity
{
    void ProcessInput();
    void SetupDisplay();
    void ClearDisplay();
    void ResetDish();
    void UpdateButtons();
    void DestroyActivity();
    void UpdateActivity(float deltaTime);
}
