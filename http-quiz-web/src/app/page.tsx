import { headers } from "next/headers";
import { PageContent } from "./pageContent";

export default function Home() {
  const headersList = headers();
  return (
    <>
      <PageContent baseUrl={process.env.NEXT_PUBLIC_API_URL!} />
    </>
  );
}
