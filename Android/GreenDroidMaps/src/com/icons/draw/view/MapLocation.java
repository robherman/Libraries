package com.icons.draw.view;

import java.io.Serializable;

/** Class to hold our location information */
public class MapLocation implements Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = 3301984086817729938L;
	private int latitude;
	private int longitude;
	private double doubleLatitude;
	private double doubleLongitude;
	private String mName;
	private String mAddress;
	private boolean isSpecial;

	public MapLocation(String name, double latitude, double longitude) {
		this.mName = name;
		this.doubleLatitude = latitude;
		this.doubleLongitude = longitude; 
		this.setLatitude((int)(latitude*1e6));
		this.setLongitude((int)(longitude*1e6));
	}
	
	public MapLocation(String name, String address, double latitude, double longitude) {
		this(name, latitude, longitude);
		this.mAddress = address;
	}

	public String getName() {
		return mName;
	}

	public void setLatitude(int latitude) {
		this.latitude = latitude;
	}

	public int getLatitude() {
		return latitude;
	}

	public void setLongitude(int longitude) {
		this.longitude = longitude;
	}

	public int getLongitude() {
		return longitude;
	}

	public void setSpecial(boolean isSpecial) {
		this.isSpecial = isSpecial;
	}

	public boolean isSpecial() {
		return isSpecial;
	}

	public void setDoubleLatitude(double doubleLatitude) {
		this.doubleLatitude = doubleLatitude;
	}

	public double getDoubleLatitude() {
		return doubleLatitude;
	}

	public void setDoubleLongitude(double doubleLongitude) {
		this.doubleLongitude = doubleLongitude;
	}

	public double getDoubleLongitude() {
		return doubleLongitude;
	}

	public void setmAddress(String mAddress) {
		this.mAddress = mAddress;
	}

	public String getmAddress() {
		return mAddress;
	}
}
