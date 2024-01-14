﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DSRotationTransmitter : IGyroTransmitter
{
    // Layout extension written in JSON
    const string LayoutJson = @"{
      ""name"": ""DualShock4GamepadHIDCustom"",
      ""extend"": ""DualShock4GamepadHID"",
      ""controls"": [
        {""name"":""gyro"", ""format"":""VC3S"", ""offset"":13,
         ""layout"":""Vector3"", ""processors"":""ScaleVector3(x=-1,y=-1,z=1)""},
        {""name"":""gyro/x"", ""format"":""SHRT"", ""offset"":0 },
        {""name"":""gyro/y"", ""format"":""SHRT"", ""offset"":2 },
        {""name"":""gyro/z"", ""format"":""SHRT"", ""offset"":4 },
        {""name"":""accel"", ""format"":""VC3S"", ""offset"":19,
         ""layout"":""Vector3"", ""processors"":""ScaleVector3(x=-1,y=-1,z=1)""},
        {""name"":""accel/x"", ""format"":""SHRT"", ""offset"":0 },
        {""name"":""accel/y"", ""format"":""SHRT"", ""offset"":2 },
        {""name"":""accel/z"", ""format"":""SHRT"", ""offset"":4 }
      ]}";

    //  Accumulation of gyro input
    Quaternion _accGyro = Quaternion.identity;

    //  Current cached rotation.
    Quaternion m_rotation = Quaternion.identity;

    public bool Setup()
    {
        if (Gamepad.current == null) return false;

        // DS4 input layout extension
        InputSystem.RegisterLayoutOverride(LayoutJson);

        // Gyroscope input callback
        var action = new InputAction(binding: "<Gamepad>/gyro");
        action.performed += ctx => _accGyro *= GyroInputToRotation(ctx);
        action.Enable();
        return true;
    }

    public Quaternion GetOrientation()
    {
        // Current status
        var rot = m_rotation;

        // Rotation from gyroscope
        rot *= _accGyro;
        _accGyro = Quaternion.identity;

        // Accelerometer input
        var accel = Gamepad.current?.GetChildControl<Vector3Control>("accel");
        var gravity = accel?.ReadValue() ?? -Vector3.up;

        // Drift compensation using gravitational acceleration
        var comp = Quaternion.FromToRotation(rot * gravity, -Vector3.up);

        // Compensation reduction
        comp.w *= 0.2f / Time.deltaTime;
        comp = comp.normalized;

        // Update
        m_rotation = comp * rot;
        return m_rotation;
    }

    private Quaternion GyroInputToRotation(in InputAction.CallbackContext ctx)
    {
        // Gyro input data
        var gyro = ctx.ReadValue<Vector3>();

        // Coefficient converting a gyro data value into a degree
        // Note: The actual constant is undocumented and unknown.
        //       I just put a plasible value by guessing.
        const double GyroToAngle = 16 * 360 / System.Math.PI;

        // Delta time from the last event
        var dt = ctx.time - ctx.control.device.lastUpdateTime;
        dt = System.Math.Min(dt, 1.0 / 60); // Discarding large deltas

        return Quaternion.Euler(gyro * (float)(GyroToAngle * dt));
    }
}