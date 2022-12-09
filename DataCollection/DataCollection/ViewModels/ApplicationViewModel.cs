using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using CameraControl;
using CameraControl.Devices;
using CameraControl.Devices.Classes;
using WebSocketSharp;
using Newtonsoft.Json;

using DataCollection.Models;
using DataCollection.ViewModels.Commands;
using System.Text.RegularExpressions;
using WIA;

namespace DataCollection.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public CameraDeviceManager CDM { get; set; }
        public string ImageFolder { get; set; } = "C:\\Users\\chuck\\Documents\\GitHub\\Neuromorphic-Vision-Capstone\\App\\SavedImages";

        private WebSocket ws;

        private string _capturedImageLocation;
        public string CapturedImageLocation
        {
            get { return _capturedImageLocation; }
            set
            {
                _capturedImageLocation = value;
                OnPropertyChanged("CapturedImageLocation");
            }
        }

        private SensorOrientation _orientation;
        public SensorOrientation Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;
                OnPropertyChanged("Orientation");
            }
        }

        public ApplicationViewModel()
        {
            // Default Data
            Orientation = new SensorOrientation() { X = 0, Y = 0, Z = 0 };

            // Initalize camera events
            CDM = new CameraDeviceManager();
            CDM.CameraSelected += CDM_CameraSelected;
            CDM.CameraConnected += CDM_CameraConnected;
            CDM.PhotoCaptured += CDM_PhotoCaptured;
            CDM.CameraDisconnected += CDM_CameraDisconnected;

            // Initialize sensor Websocket
            string socketAddress = "ws://192.168.0.234:8081/sensor/connect?type=android.sensor.orientation";
            ws = new WebSocket(socketAddress);
            ws.OnMessage += Ws_OnMessage;
            ws.Connect();

        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            var recieved = JsonConvert.DeserializeObject<SensorSocketData>(e.Data);
            Orientation = new SensorOrientation()
            {
                X = recieved.Values[0],
                Y = recieved.Values[1],
                Z = recieved.Values[2]
            };
        }

        private void CDM_PhotoCaptured(object sender, CameraControl.Devices.Classes.PhotoCapturedEventArgs eventArgs)
        {
            // to prevent UI freeze start the transfer process in a new thread
            Thread thread = new Thread(PhotoCaptured);
            thread.Start(eventArgs);
        }

        private void PhotoCaptured(object o)
        {
            PhotoCapturedEventArgs eventArgs = o as PhotoCapturedEventArgs;
            if (eventArgs == null)
                return;
            try
            {
                string fileName = Path.Combine(ImageFolder, Path.GetFileName(eventArgs.FileName));
                // if file exist try to generate a new filename to prevent file lost. 
                // This useful when camera is set to record in ram the the all file names are same.
                if (File.Exists(fileName))
                    fileName =
                      StaticHelper.GetUniqueFilename(
                        Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName) + "_", 0,
                        Path.GetExtension(fileName));

                // check the folder of filename, if not found create it
                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }
                eventArgs.CameraDevice.TransferFile(eventArgs.Handle, fileName);
                // the IsBusy may used internally, if file transfer is done should set to false  
                eventArgs.CameraDevice.IsBusy = false;
                // Capture_Image.ImageLocation = fileName;
            }
            catch (Exception exception)
            {
                eventArgs.CameraDevice.IsBusy = false;
                MessageBox.Show("Error download photo from camera :\n" + exception.Message);
            }
        }

        private void CDM_CameraDisconnected(ICameraDevice cameraDevice)
        {
        }

        private void CDM_CameraConnected(ICameraDevice cameraDevice)
        {
        }

        private void CDM_CameraSelected(ICameraDevice oldcameraDevice, ICameraDevice newcameraDevice)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private RelayCommand _captureCommand;
        public RelayCommand CaptureCommand
        {
            get
            {
                return _captureCommand ??
                    (_captureCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            // Do the image capturing
                            bool retry;
                            do
                            {
                                retry = false;
                                try
                                {
                                    CDM.SelectedCameraDevice.CapturePhoto();
                                }
                                catch (DeviceException exception)
                                {
                                    // if device is bussy retry after 100 miliseconds
                                    if (exception.ErrorCode == ErrorCodes.MTP_Device_Busy ||
                                        exception.ErrorCode == ErrorCodes.ERROR_BUSY)
                                    {
                                        // !!!!this may cause infinite loop
                                        Thread.Sleep(100);
                                        retry = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error occurred :" + exception.Message);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error occurred :" + ex.Message);
                                }

                            } while (retry);

                            // Save the image

                        }
                        catch (Exception ex)
                        {

                        }
                    }));
            }
        }

        private void Capture_Button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(Capture);
            thread.Start();
        }

        private void Capture()
        {

        }
    }
}
