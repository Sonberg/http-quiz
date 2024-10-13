import * as express from "express";

const app = express();
const port = 8000;

app.use(express.json());

app.post('/calculate/addition', (req, res) => {
  res.json(req.body.input.num1 + req.body.input.num2);
});

app.use((req, res) => {
  console.log({
    method: req.method,
    path: req.path,
    body: req.body,
    query: req.query,
    params: req.params,
  });

  res.sendStatus(200);
});

app.listen(port, () => {
  console.log(`Example app listening on port ${port}`);
});
