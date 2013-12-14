package com.baijiayi.microassistant;


import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnKeyListener;
import android.widget.Toast;

import org.apache.cordova.*;

public class MainActivity extends DroidGap {
	private long mExitTime=0;
    @Override
    public void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        //super.setIntegerProperty("splashscreen",R.drawable.splash );
        super.loadUrl(Config.getStartUrl(),5000);
        /*super.appView.setOnKeyListener(new OnKeyListener() {						
        	@Override			
        	public boolean onKey(View v, int keyCode, KeyEvent event) {	
                if (keyCode == KeyEvent.KEYCODE_BACK) {
                    if ((System.currentTimeMillis() - mExitTime) > 2000) {
                        Object mHelperUtils;
                        Toast.makeText(PICC_Android.this, "再按一次退出程序", Toast.LENGTH_SHORT).show();
                        mExitTime = System.currentTimeMillis();
		              } else if((System.currentTimeMillis() - mExitTime) > 500) {
		                      finish();
		              }
               return true;
                }
        		return false;	
        		}	
        	});*/
    }

}
