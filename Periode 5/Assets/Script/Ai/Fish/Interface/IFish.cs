using UnityEngine;

public interface IFish
{
    string M_Name { get; set; }

    void Atteract(Vector3 destination);
    void Catched();
    void BeingCatched();
    void Escaped();
}
