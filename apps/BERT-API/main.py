from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from transformers import AutoTokenizer, TFAutoModelForSequenceClassification
import tensorflow as tf

# تحميل الموديل والـ tokenizer
model = TFAutoModelForSequenceClassification.from_pretrained("model")
tokenizer = AutoTokenizer.from_pretrained("model")

app = FastAPI()

# شكل الطلب
class TextInput(BaseModel):
    text: str

@app.post("/predict")
def predict(input: TextInput):
    try:
        inputs = tokenizer(input.text, return_tensors="tf", padding=True, truncation=True, max_length=512)
        outputs = model(inputs)
        predictions = tf.nn.softmax(outputs.logits, axis=1)
        predicted_class = tf.argmax(predictions, axis=1).numpy()[0]
        confidence = float(tf.reduce_max(predictions).numpy())

        return {
            "class": int(predicted_class),
            "confidence": confidence
        }
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))