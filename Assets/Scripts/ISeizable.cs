using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISeizable
{
    void OnSeize();
    void OnUnseize();

    bool Tiltable();
}
