# VideoFragmentCodingChallenge
Objective: find the total amount of time watched from a collection of video fragment timestamps

## What it is:
The purpose of this challenge is to calculate the total amount of time a viewer has watched portions of a video. For example, if a viewer watches the first 100ms of a video and the last 100ms of a video, the program would return 200mn (the sum of the fragments). Likewise, if a viewer watched the first 100ms of a video, then 50ms-150ms, the program would return 150ms (the sum of the fragments, not counting the overlap)

## How to use it:
Using a commandline, navigate to the directory and execute `.\VideoFragmentCodingChallenge.exe <file>`, where <file> contains the fragmented times in ms. For example, say you had a file `myTestData.txt` located in the same folder as the exe, with the following content:

```
8:10
0:1
6:8
2:4
3:5
2:3
```
(note the lack of order; this viewer skipped around in the video)

The run command would look like this:
`.\VideoFragmentCodingChallenge.exe .\myTestData.txt`

And the output would follow:

```
Processing Fragments from file .\myTestData.txt
Total time of fragments: 8
```
