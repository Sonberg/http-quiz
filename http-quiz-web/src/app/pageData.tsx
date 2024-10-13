import { getApi } from "@/lib/api";
import { QueryClient, useMutation } from "@tanstack/react-query";
import { useMemo } from "react";

const client = new QueryClient();

type Args = {
  baseUrl: string;
};

export function usePageData({ baseUrl }: Args) {
  const Api = useMemo(() => getApi(baseUrl), [baseUrl]);

  const setStarted = useMutation(
    {
      mutationFn: async (state: boolean) => {
        await Api.post(state ? "/api/start" : "/api/stop");
      },
    },
    client
  );

  const setDelay = useMutation(
    {
      mutationFn: async (delay: number) => {
        await Api.put("/api/set-delay", { delay });
      },
    },
    client
  );

  const setLevel = useMutation(
    {
      mutationFn: async (level: number) => {
        await Api.put("/api/set-level", { level });
      },
    },
    client
  );


  const clear = useMutation(
    {
      mutationFn: async () => {
        await Api.post("/api/clear");
      },
    },
    client
  );


  return {
    setDelay: setDelay.mutate,
    setLevel: setLevel.mutate,
    setStarted: setStarted.mutate,
    clear: clear.mutate
  };
}
