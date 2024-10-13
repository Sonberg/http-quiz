import { getApi } from "@/lib/api";
import { QueryClient, useMutation } from "@tanstack/react-query";

const client = new QueryClient();

type Args = {
  baseUrl: string;
};

export function usePageData({ baseUrl }: Args) {
  const setStarted = useMutation(
    {
      mutationFn: async (state: boolean) => {
        await getApi(baseUrl).post(state ? "/api/start" : "/api/stop");
      },
    },
    client
  );

  const setDelay = useMutation(
    {
      mutationFn: async (delay: number) => {
        await getApi(baseUrl).put("/api/set-delay", { delay });
      },
    },
    client
  );

  const setLevel = useMutation(
    {
      mutationFn: async (level: number) => {
        await getApi(baseUrl).put("/api/set-level", { level });
      },
    },
    client
  );

  return {
    setDelay: setDelay.mutate,
    setLevel: setLevel.mutate,
    setStarted: setStarted.mutate,
  };
}
