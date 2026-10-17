from transformers import pipeline

classifier = pipeline(
    "audio-classification",
    model="dima806/music_genres_classification"
)

result = classifier("test.wav", top_k=3)

print(result)