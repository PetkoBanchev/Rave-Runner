using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioController : MonoBehaviour {
	AudioSource _audioSource;

	float[] samples = new float[512];
	float[] freqBands = new float[8];

	float[] freqBandsHighest = new float[8];
	float[] buffers = new float[8];
	float[] bufferDecrease = new float[8];
	public static float[] audioBands = new float[8];

	public static float[] audioBandsBuffer = new float[8];
	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		GetAudioSourceSpectrum();
		buffer();
		CreateAudioBands();
	}
	/**
	Getting Spectrum data
	*/	
	void GetAudioSourceSpectrum() {
		_audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
		GetFrequencyBands();
	}

	/**
	Dividing all the samples into 8 manageble frequency bands 
	*/
	void GetFrequencyBands() {

		int count = 0;

		for (int i = 0; i < 8; i++)
		{
			int sampleCount = (int)Mathf.Pow(2,i)*2;
			float avg = 0;

			//Adding up to 512 samples
			if (i == 7)
			{
				sampleCount +=2;
			}

			//Averaging out the frequencies, to minimize the drastic differences
			for (int j = 0; j < sampleCount; j++)
			{
				avg += samples[count] * (count + 1);
				count++;
			}

			avg /= count;
			freqBands[i] = avg * 10; 
		}
	}
	/**
	Creating 8 audio bands with values between 0 and 1	
	 */
	void CreateAudioBands() {
		for (int i = 0; i < 8; i++)
		{
			if (freqBands[i] > freqBandsHighest[i])
			{
				freqBandsHighest[i] = freqBands[i];
			}

			audioBands[i] = (freqBands[i]/freqBandsHighest[i]);
			audioBandsBuffer[i] = (buffers[i]/freqBandsHighest[i]);
		}
	}
	void buffer() {
		for (int i = 0; i < 8; i++)
		{
			if (freqBands[i] > buffers[i])
			{
				buffers[i] = freqBands[i];
				bufferDecrease[i] = 0.005f;
			}

			if (freqBands[i] < buffers[i])
			{
				buffers[i] -= bufferDecrease[i];
				bufferDecrease[i] *= 1.2f;
			}
		}
	}
}
