﻿using KSerialization;
using STRINGS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static EventSystem;

namespace EdgeDetectors
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class DiodeGate : UnaryGate
    {
        public override void LogicTick()
        {
            if (GetInputNetwork() == null || GetOutputNetwork() == null)
            {
                kbac.Play((HashedString)"off");
                return;
            }

            LastInput = CurrentInput;
            CurrentInput = GetInputValue();
            CurrentOutput = CurrentInput;
            if (CurrentOutput != LastOutput)
            {
                this.GetComponent<LogicPorts>().SendSignal(DiodeGateConfig.OUTPUT_PORT_ID, CurrentOutput);
            }
            UpdateVisuals(CurrentInput, CurrentOutput);
        }
    }
}
