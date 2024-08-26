export function Instructions() {
  return (
    <div className="font-mono">
      <h3 className="text-xl mb-4 mt-8">Recommended setup</h3>
      <ul className="list-disc mb-12 list-inside">
        <li>A local web server</li>
        <li>Hot reload</li>
        <li>
          Ability to see all requests that are incoming to you server. Url,
          method & payload etc
        </li>
        <li>You can use any language and tool</li>
        <li>
          I would recommend Python (Flask) or JavaScript (Express + Nodemon)
        </li>
      </ul>

      <h3 className="text-xl mb-4 mt-8">How does it work?</h3>
      <ol className="list-decimal mb-12 list-inside">
        <li>Use register you web server by providing your port number</li>
        <li>Host will fire a request a every participant</li>
        <li>How well you answer to that request will decide your score.</li>
        <li>
          The faster you implement new requsts, the fast will your scope
          increase
        </li>
        <li>Request will get harder with every level and quicker</li>
        <li>Happy coding</li>
      </ol>
    </div>
  );
}
