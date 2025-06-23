# AI-Human Text Detector

This project is a full-stack application designed to distinguish between human-written and AI-generated text. It consists of a Next.js frontend, a .NET Core backend, and a Python-based BERT model API for text classification.

## Running the Project

To run the entire project, you will need to start each of the three main components: the frontend, the backend, and the BERT API. Below are the instructions for each part.

### 1. Frontend (Next.js)

The frontend is a Next.js application.

1.  **Navigate to the frontend directory:**

    ```bash
    cd apps/ai-human_front
    ```

2.  **Install dependencies:**

    ```bash
    npm install
    ```

3.  **Run the development server:**

    ```bash
    npm run dev
    ```

    The frontend will be available at `http://localhost:3000`.

### 2. Backend (.NET Core)

The backend is an ASP.NET Core Web API.

1.  **Navigate to the backend API directory:**

    ```bash
    cd apps/ai_human_backend/API
    ```

2.  **Run the application:**

    ```bash
    dotnet run
    ```

    The backend API will be running and accessible to the frontend,it will beavailable at `http://localhost:7162`.


### 3. BERT API (Python/FastAPI)

The BERT model is served via a FastAPI application.

1.  **Navigate to the BERT API directory:**

    ```bash
    cd apps/BERT-API
    ```

2.  **Install Python dependencies:**

    ```bash
    pip install -r requirements.txt
    ```

3.  **Run the FastAPI server:**

    ```bash
    uvicorn main:app --reload
    ```

    The BERT API will be available at `http://localhost:8000`.

---

Once all three services are running, you can open your browser to `http://localhost:3000` to use the application.