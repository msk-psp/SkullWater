package com.kpu.burniture;

import android.app.Activity;
import android.app.Dialog;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

import java.util.Locale;

public class MainActivity{
    Toast toast;
    protected Activity mCurrentActivity;
    public void setActivity(Activity activity)
    {
        mCurrentActivity = activity;
    }

    public void ToastM()
    {
        Toast.makeText(mCurrentActivity,"한번 더 누르면 종료됩니다.",Toast.LENGTH_SHORT).show();
    }
    public void ToastW()
    {
        Toast.makeText(mCurrentActivity,"벽이 이미 존재합니다.",Toast.LENGTH_SHORT).show();
    }

    public void EscapeMessage()
    {
        handler.sendEmptyMessage(0);
    }
    public void WallCheckMessage(){handler.sendEmptyMessage(1);}

    Handler handler=new Handler(){
      public void handleMessage(Message msg){
          switch(msg.what){
              case 0:
                  ToastM();
                  break;
              case 1:
                  ToastW();
                  break;
              default:
                  break;
          }
      }
    };
}
