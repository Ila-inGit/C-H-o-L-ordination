import numpy as np
import os
import librosa
from librosa import frames_to_samples
import soundfile as sf
import matplotlib.pyplot as plt

os.chdir(os.path.abspath(os.path.dirname(__file__)))

# This is useful to change the current directory to the one where there is the file


DATA_DIR = "./data"
assert os.path.exists(DATA_DIR), "wrong data dir"
# %% Define filenames
filename_in = os.path.join(
    DATA_DIR, "tire_swings.wav")  # put whatever you like
filename_kick = os.path.join(DATA_DIR, "kick.wav")  # put whatever you like
filename_out = os.path.join(DATA_DIR, "tire_disco.wav")

# %% 1) Load a song
SR = 16000  # sample rate => sample per second, quanti sample fa per secondo
# 1/SR * beat => time in which the sample is taken
y = np.zeros((3,))
# your code here
y, sr = librosa.load(filename_in, sr=SR)

# %% 2) Find the beats
tempo, beats = librosa.beat.beat_track(y, sr, units="samples")
print("Beats: ", beats)
seconds = []
for b in beats:
    seconds.append(1/SR * b)
print("Time: ", seconds)
print("BPM: ", len(seconds)/24*60)

outF = open("D:/POLI_sw/Unity/projects/MixTry/Assets/MyOutput.txt", "w")
outF.write("BPM:" + str(len(seconds)/24*60))
for s in seconds:
    # write line to output file
    outF.write(str(s))
    outF.write("\n")
outF.close()
# %%
