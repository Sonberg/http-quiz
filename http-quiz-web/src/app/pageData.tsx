import { Api } from "@/lib/api";
import { QueryClient, useMutation } from "@tanstack/react-query";

const client = new QueryClient();

export function usePageData() {
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

  return {
    setDelay: setDelay.mutate,
    setLevel: setLevel.mutate,
    setStarted: setStarted.mutate,
  };
}
