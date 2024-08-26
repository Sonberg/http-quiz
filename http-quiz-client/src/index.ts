import * as express from "express";

const app = express();
const port = 8000;

app.use(express.json());

app.use((req, res) => {
  console.log({
    method: req.method,
    path: req.path,
    body: req.body,
    query: req.query,
    params: req.params,
  });

  res.json({});
});

app.listen(port, () => {
  console.log(`Example app listening on port ${port}`);
});
