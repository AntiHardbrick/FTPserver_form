using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ToastManager {

    private static ToastManager instance = null;

    List<CancellationTokenSource> toast_cancelSourceList;//for function: ShowToast()
    int toast_running_index = -1;
    ToolStripStatusLabel StatusLabel = null;
    Form Mainform;

    public ToastManager(ToolStripStatusLabel label_item, Form mainform) {
        StatusLabel = label_item;
        Mainform = mainform;
        toast_cancelSourceList = new List<CancellationTokenSource>();
    }

    #region wpf
    //for wpf
    /*
    public void ShowToast(string log, Brush status_color, int millisecond = 5000) {
        int index = -1;
        for (int i = 0; i < toast_cancelSourceList.Count; i++) {
            if (toast_cancelSourceList[i] == null) {
                index = i;
                toast_cancelSourceList[i] = new CancellationTokenSource();
                break;
            }
        }
        if (index == -1) {
            toast_cancelSourceList.Add(new CancellationTokenSource());
            index = toast_cancelSourceList.Count - 1;
        }

        if (toast_running_index != -1) {
            toast_cancelSourceList[toast_running_index].Cancel();
        }
        toast_running_index = index;

        //toast_source = new CancellationTokenSource();
        Task.Run(() => {
            //MainWindow.instance.Dispatcher.Invoke(() => MainWindow.instance.label_status.Foreground = status_color);
            //MainWindow.instance.Dispatcher.Invoke(() => MainWindow.instance.label_status.Content = log);

            Task.Delay(millisecond).Wait();
            if (!toast_cancelSourceList[index].IsCancellationRequested) {
                MainWindow.instance.Dispatcher.Invoke(() => MainWindow.instance.label_status.Content = "");
                toast_running_index = -1;
            }
            toast_cancelSourceList[index].Dispose();
            toast_cancelSourceList[index] = null;
            return;
        });
    */
    #endregion
    

    #region winform
    public void ShowToast(string log, Color status_color=default(Color), int millisecond = 5000) {
        int index = -1;
        for (int i = 0; i < toast_cancelSourceList.Count; i++) {
            if (toast_cancelSourceList[i] == null) {
                index = i;
                toast_cancelSourceList[i] = new CancellationTokenSource();
                break;
            }
        }
        if (index == -1) {
            toast_cancelSourceList.Add(new CancellationTokenSource());
            index = toast_cancelSourceList.Count - 1;
        }

        if (toast_running_index != -1) {
            toast_cancelSourceList[toast_running_index].Cancel();
        }
        toast_running_index = index;

        //toast_source = new CancellationTokenSource();
        Task.Run(() => {
            Mainform.Invoke(() => StatusLabel.ForeColor = status_color);
            Mainform.Invoke(() => StatusLabel.Text = log);
            Task.Delay(millisecond).Wait(); //for waiting all tasks are cancelled?
            if (!toast_cancelSourceList[index].IsCancellationRequested) {
                Mainform.Invoke(() => StatusLabel.Text = "");
                toast_running_index = -1;
            }

            toast_cancelSourceList[index].Dispose();
            toast_cancelSourceList[index] = null;
            return;
        });
    }
    #endregion
}
