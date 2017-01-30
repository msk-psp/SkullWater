package com.kpu.burniture;

import android.app.Activity;
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

    /*public void onBackPressed()
    {
        long Time=0;
        toast=Toast.makeText(mCurrentActivity,"한번 더 누르면 종료됩니다.",Toast.LENGTH_SHORT);
        if(System.currentTimeMillis()>Time+2000){
            Time=System.currentTimeMillis();
            toast.show();
            return;
        }
        if(System.currentTimeMillis()<=Time+2000){
            mCurrentActivity.finishAffinity();
            toast.cancel();
            android.os.Process.killProcess(android.os.Process.myPid());
        }
    }*/
    public void ToastM()
    {
        Toast.makeText(mCurrentActivity,"한번 더 누르면 종료됩니다.",Toast.LENGTH_SHORT).show();
    }

    public void EscapeMessage()
    {
        handler.sendEmptyMessage(0);
    }

    Handler handler=new Handler(){
      public void handleMessage(Message msg){
          switch(msg.what){
              case 0:
                  //onBackPressed();
                  ToastM();
                  break;
              default:
                  break;
          }
      }
    };
}
