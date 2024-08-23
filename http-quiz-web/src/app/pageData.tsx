import { Api } from "@/lib/api";
import { QueryClient, useMutation, useQuery } from "@tanstack/react-query";

const client = new QueryClient();

type TeamScore = {
  teamId: string;
  teamName: string;
  points: number;
};

type Stats = {
  isStarted: boolean;
  round: number;
  level: number;
  delay: number;
  levels: number[];
};

export function usePageData() {
  const leaderboard = useQuery(
    {
      queryKey: ["leaderboard"],
      queryFn: async () =>
        (await Api.get<TeamScore[]>("/api/leaderboard")).data,
      initialData: [],
    },
    client
  );

  const stats = useQuery(
    {
      queryKey: ["states"],
      queryFn: async () => (await Api.get<Stats>("/api/stats")).data,
      initialData: {
        isStarted: false,
        delay: 30,
        level: 1,
        round: 0,
        levels: [],
      },
    },
    client
  );

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
    stats: stats.data,
    leaderboard: leaderboard.data,
    setDelay: setDelay.mutate,
    setLevel: setLevel.mutate,
    setStarted: setStarted.mutate,
    refresh: async () => {
      await Promise.all([stats.refetch(), leaderboard.refetch()]);
    },
  };
}
