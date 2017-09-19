﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterStates
{
    void InitializeState();

    void UpdateState();

    void OnTriggerStay(Collider collider);
}