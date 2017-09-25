using UnityEngine;

public interface IFish
{
    void Atteract(Vector3 destination);
    void Catched();
    void BeingCatched();
    void Escaped();
}
