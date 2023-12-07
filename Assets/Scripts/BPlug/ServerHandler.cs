using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buttplug.Client;
using Buttplug.Client.Connectors.WebsocketConnector;
using Buttplug.Core.Messages;
using System.Security.Cryptography;
using System;

public class ServerHandler : MonoBehaviour
{
    public static ServerHandler Instance {
        get;
        private set;
    }

    [SerializeField, Range(0f, 1f)] private float intensity = 0f;
    private ButtplugClient client;

    public List<ButtplugClientDevice> devices { get; } = new List<ButtplugClientDevice>();

    private void OnEnable()
    {
        Instance = this;
    }

    public void SetIntensity(float value)
    {
        intensity = Mathf.Clamp(value, 0f, 1f);
    }

    private async void Start()
    {
        client = new ButtplugClient("Test Client");
        Log("Trying to create client");

        //Set up client event handlers before connection. 
        client.DeviceAdded += AddDevice;
        client.DeviceRemoved += RemoveDevice;
        client.ScanningFinished += ScanFinished;

        //Create a Websocket Connector. 
        var connector = new ButtplugWebsocketConnector(
            new System.Uri("ws://localhost:12345/buttplug")
            );
        await client.ConnectAsync( connector );
        
        await client.StartScanningAsync();
    }

    private async void OnDestroy()
    {
        devices.Clear();

        //On object shutdown disconnect the client and just kill the server process. 

        if(client != null)
        {
            client.DeviceAdded -= AddDevice;
            client.DeviceRemoved -= RemoveDevice;
            client.ScanningFinished -= ScanFinished; 
            await client.DisconnectAsync();

            client.Dispose();
            client = null; 
        }

        Log("Client server shutdown");

    }

    private void UpdateDevices()
    {
        foreach(ButtplugClientDevice device in devices)
        {
            device.VibrateAsync(intensity);
        }
    }

    private void LateUpdate()
    {
        UpdateDevices();
    }

    private void OnValidate()
    {
        UpdateDevices();
    }

    private void AddDevice(object sender, DeviceAddedEventArgs e)
    {
        Log($"Device {e.Device.Name} Connected!");
        devices.Add(e.Device);
        UpdateDevices();
    }

    private void RemoveDevice(object sender, DeviceRemovedEventArgs e)
    {
        Log($"Device {e.Device.Name} Removed!");
        devices.Remove(e.Device);
        UpdateDevices();
    }

    private void ScanFinished(object sender, EventArgs e)
    {
        Log("Device scanning completed!");
    }

    private void Log(object text)
    {
        Debug.Log("<color=red>Buttplug:</color> " + text, this);
    }
}
