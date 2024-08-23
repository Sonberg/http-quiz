import { headers } from "next/headers";
import { PageContent } from "./pageContent";

export default function Home() {
  const headersList = headers();
  return (
    <>
      <PageContent
        ipAddress={headersList.get("x-forwarded-for")!.replace("::ffff:", "")}
      />
    </>
  );
}
