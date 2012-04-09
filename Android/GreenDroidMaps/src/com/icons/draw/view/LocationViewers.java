package com.icons.draw.view;

import java.util.ArrayList;
import java.util.List;
import android.content.Context;
import android.util.AttributeSet;
import android.widget.LinearLayout;
import com.google.android.maps.GeoPoint;
import com.google.android.maps.MapView;
import com.icons.draw.R;

public class LocationViewers extends LinearLayout {

	private MapLocationOverlay overlay;
	
    //  Known lat/long coordinates that we'll be using.
    private List<MapLocation> mapLocations;
    
    public static MapView mapView;
    
	public LocationViewers(Context context, AttributeSet attrs) {
		super(context, attrs);
		init();
	}

	public LocationViewers(Context context, List<MapLocation> mapLocations) {
		super(context);
		this.mapLocations = mapLocations;
		init();
		this.setAutoZoom();
	}
	
	public LocationViewers(Context context, MapLocation mapLocation) {
		super(context);
		
		this.mapLocations = new ArrayList<MapLocation>();
		this.mapLocations.add(mapLocation);
		
		init();
		
		mapView.getController().setZoom(16);
    	mapView.getController().setCenter(new GeoPoint(mapLocation.getLatitude(), mapLocation.getLongitude()));
	}
	
	public void init() {		

		setOrientation(VERTICAL);
		setLayoutParams(new LinearLayout.LayoutParams(android.view.ViewGroup.LayoutParams.FILL_PARENT,android.view.ViewGroup.LayoutParams.FILL_PARENT));
		String api = getResources().getString(R.string.map_api_key);
		mapView = new MapView(getContext(), api);
		mapView.setEnabled(true);
		mapView.setClickable(true);
		mapView.setBuiltInZoomControls(true);
		addView(mapView);
		overlay = new MapLocationOverlay(this);
		mapView.getOverlays().add(overlay);
    	
	}
	
	public List<MapLocation> getMapLocations() {
		return mapLocations;
	}

	public MapView getMapView() {
		return mapView;
	}
	
	private void setAutoZoom() {
		int minLat = Integer.MAX_VALUE;
		int maxLat = Integer.MIN_VALUE;
		int minLon = Integer.MAX_VALUE;
		int maxLon = Integer.MIN_VALUE;

		for (MapLocation item : this.mapLocations) 
		{ 

		      int lat = item.getLatitude();
		      int lon = item.getLongitude();

		      maxLat = Math.max(lat, maxLat);
		      minLat = Math.min(lat, minLat);
		      maxLon = Math.max(lon, maxLon);
		      minLon = Math.min(lon, minLon);
		}

		mapView.getController().zoomToSpan(Math.abs(maxLat - minLat), Math.abs(maxLon - minLon));
		mapView.getController().animateTo(new GeoPoint( (maxLat + minLat)/2, 
		(maxLon + minLon)/2 )); 
	}
}
