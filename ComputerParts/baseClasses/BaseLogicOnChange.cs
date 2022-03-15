﻿using KSerialization;
using STRINGS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static EventSystem;

namespace KBComputing.baseClasses
{
    [SerializationConfig(MemberSerialization.OptIn)]
    abstract class BaseLogicOnChange : KMonoBehaviour
        , ILogicEventSender
        , ILogicEventReceiver
        , ILogicNetworkConnection
        , ISaveLoadable
        {

        private static readonly EventSystem.IntraObjectHandler<BaseLogicOnChange>
            OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<BaseLogicOnChange>(
            (component, data) => component.OnLogicValueChanged(data)
            );

        protected LogicPorts ports;
        protected KBatchedAnimController kbac;

        protected override void OnSpawn()
        {
            base.OnSpawn();
            gameObject.AddOrGet<CopyBuildingSettings>();
            Subscribe((int)GameHashes.LogicEvent, OnLogicValueChangedDelegate);
            this.ports = this.GetComponent<LogicPorts>();
            this.kbac = this.GetComponent<KBatchedAnimController>();
        }

        public void OnLogicNetworkConnectionChanged(bool connected)
        {
            OnLogicValueChanged(null);
        }


        public void OnLogicValueChanged(object data)
        {
            if (UpdateValues())
                UpdateVisuals();
        }

        public abstract bool UpdateValues();

        public abstract void UpdateVisuals();

        public void LogicTick()
        {
            OnLogicValueChanged(null);
        }

        public int GetLogicCell()
        {
            return 0;
        }

        public int GetLogicValue()
        {
            return 0;
        }

        public void ReceiveLogicEvent(int value)
        {
            OnLogicValueChanged(null);
        }
    }
}
