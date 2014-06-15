package com.baijiayi.app.ui;
import com.baijiayi.app.R;
import java.io.File;
import java.io.IOException;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.ViewSwitcher;

import com.baijiayi.app.AppException;
import com.baijiayi.app.api.ApiClient;
import com.baijiayi.app.common.FileUtils;
import com.baijiayi.app.common.ImageUtils;
import com.baijiayi.app.common.StringUtils;
import com.baijiayi.app.common.UIHelper;

/**
 * 图片对话框
 * @author liux 
 * @version 1.0
 * @created 2012-3-21
 */
public class ImageDialog extends Activity{
	
	private ViewSwitcher mViewSwitcher;
	private ImageButton btn_close;
	private ImageView mImage;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.image_dialog);
        
        this.initView();
        
        this.initData();
    }
    
    private void initView()
    {
    	mViewSwitcher = (ViewSwitcher)findViewById(R.id.imagedialog_view_switcher); 
    	
    	btn_close = (ImageButton)findViewById(R.id.imagedialog_close_button);
        btn_close.setOnClickListener(UIHelper.finish(this));
       
        mImage = (ImageView)findViewById(R.id.imagedialog_image);
    }    
    
    private void initData() 
    {
		final String imgURL = getIntent().getStringExtra("img_url");		
		final String ErrMsg = getString(R.string.msg_load_image_fail);
		final Handler handler = new Handler(){
			@Override
			public void handleMessage(Message msg) {
				if(msg.what==1 && msg.obj != null){
					mImage.setImageBitmap((Bitmap)msg.obj);
					mViewSwitcher.showNext();
				}else{
					UIHelper.ToastMessage(ImageDialog.this, ErrMsg);
					finish();
				}
			}
		};
		new Thread(){
			@Override
			public void run() {
				Message msg = new Message();
				Bitmap bmp = null;
		    	String filename = FileUtils.getFileName(imgURL);
				try {
					//读取本地图片
					if(imgURL.endsWith("portrait.gif") || StringUtils.isEmpty(imgURL)){
						bmp = BitmapFactory.decodeResource(mImage.getResources(), R.drawable.widget_dface);
					}
					if(bmp == null){
						//是否有缓存图片
				    	//Environment.getExternalStorageDirectory();返回/sdcard
				    	String filepath = getFilesDir() + File.separator + filename;
						File file = new File(filepath);
						if(file.exists()){
							bmp = ImageUtils.getBitmap(mImage.getContext(), filename);
							if(bmp != null){
								//缩放图片
								bmp = ImageUtils.reDrawBitMap(ImageDialog.this, bmp);
							}
				    	}
					}
					if(bmp == null){
						bmp = ApiClient.getNetBitmap(imgURL);
						if(bmp != null){
							try {
		                    	//写图片缓存
								ImageUtils.saveImage(mImage.getContext(), filename, bmp);
							} catch (IOException e) {
								e.printStackTrace();
							}
							//缩放图片
							bmp = ImageUtils.reDrawBitMap(ImageDialog.this, bmp);
						}
					}
					msg.what = 1;
					msg.obj = bmp;
				} catch (AppException e) {
					e.printStackTrace();
	            	msg.what = -1;
	            	msg.obj = e;
				}
				handler.sendMessage(msg);
			}
		}.start();
    }
}
