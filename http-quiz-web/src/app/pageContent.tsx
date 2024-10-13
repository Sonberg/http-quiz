"use client";

import { useEffect, useState } from "react";
import { AddTeamSheet } from "@/components/AddTeamSheet";
import { Leaderboard } from "@/components/Leaderboard";
import {
  Card,
  CardHeader,
  CardDescription,
  CardTitle,
  CardContent,
} from "@/components/ui/card";
import { getSignalR } from "@/lib/signalR";
import { HubConnectionState } from "@microsoft/signalr";
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { useSearchParams } from "next/navigation";
import { Button } from "@/components/ui/button";
import { usePageData } from "./pageData";
import { Instructions } from "@/components/Instructions";

const delays = [1, 2, 3, 4, 5, 8, 10, 12, 15, 20, 30, 40, 50, 60];

type TeamScore = {
  teamId: string;
  teamName: string;
  points: number;
};

type State = {
  leaderboard: TeamScore[];
  isStarted: boolean;
  round: number;
  level: number;
  delay: number;
  levels: number[];
};

type Props = {
  baseUrl: string;
};

export function PageContent({ baseUrl }: Props) {
  const [timeLeft, setTimeLeft] = useState("-");
  const [state, setState] = useState<State>({
    leaderboard: [],
    isStarted: false,
    delay: 30,
    level: 1,
    round: 0,
    levels: [],
  });

  const params = useSearchParams();
  const { setDelay, setLevel, setStarted } = usePageData({ baseUrl });

  useEffect(() => {
    const onTimeLeft = (value: string) => {
      setTimeLeft(value);
    };

    const onState = (state: State) => {
      setState(state);
    };

    const SignalR = getSignalR(baseUrl);

    if (SignalR.state === HubConnectionState.Disconnected) {
      SignalR.start();
    }

    SignalR.on("State", onState);
    SignalR.on("TimeLeft", onTimeLeft);

    return () => {
      SignalR.off("State", onState);
      SignalR.off("TimeLeft", onTimeLeft);
      SignalR.stop();
    };

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <>
      <div className="flex items-center gap-2 font-mono mb-12">
        <img src="/logo.webp" height={54} alt="Logo" width={54} />{" "}
        <div className="text-4xl font-bold">HttpQuiz</div>
      </div>
      <div className="flex justify-between">
        <h1 className="text-2xl mb-4  font-mono">Leaderboard</h1>
      </div>
      <Leaderboard teams={state.leaderboard} />
      <AddTeamSheet baseUrl={baseUrl} />
      {params.get("admin") ? (
        <Button
          className="ml-4"
          variant="secondary"
          onClick={() => setStarted(!state.isStarted)}
        >
          {state.isStarted ? "Stop" : "Start"}
        </Button>
      ) : null}

      <div className="grid grid-flow-col auto-cols-fr gap-4 mt-8">
        <Card x-chunk="dashboard-05-chunk-1">
          <CardHeader className="pb-2">
            <CardDescription>Round</CardDescription>
            <CardTitle className="text-4xl">{state.round}</CardTitle>
          </CardHeader>
          <CardContent>{state.delay} seconds delay</CardContent>
        </Card>
        <Card x-chunk="dashboard-05-chunk-1">
          <CardHeader className="pb-2">
            <CardDescription>Level</CardDescription>
            <CardTitle className="text-4xl">{state.level}</CardTitle>
          </CardHeader>
          {params.get("admin") ? (
            <CardContent>
              <Select
                value={`${state.level}`}
                onValueChange={(value) => setLevel(parseInt(value, 10))}
              >
                <SelectTrigger className="w-[180px]">
                  <SelectValue placeholder="Select level" />
                </SelectTrigger>
                <SelectContent>
                  <SelectGroup>
                    {state.levels.map((x) => (
                      <SelectItem key={x} value={`${x}`}>
                        {x}
                      </SelectItem>
                    ))}
                  </SelectGroup>
                </SelectContent>
              </Select>
            </CardContent>
          ) : null}
        </Card>
        <Card x-chunk="dashboard-05-chunk-1">
          <CardHeader className="pb-2">
            <CardDescription>Next round in</CardDescription>
            <CardTitle className="text-4xl">{timeLeft}</CardTitle>
          </CardHeader>
          {params.get("admin") ? (
            <CardContent>
              <Select
                value={`${state.delay}`}
                onValueChange={(value) => setDelay(parseInt(value, 10))}
              >
                <SelectTrigger className="w-[180px]">
                  <SelectValue placeholder="Select delay" />
                </SelectTrigger>
                <SelectContent>
                  <SelectGroup>
                    {delays.map((x) => (
                      <SelectItem key={x} value={`${x}`}>
                        {x}s
                      </SelectItem>
                    ))}
                  </SelectGroup>
                </SelectContent>
              </Select>
            </CardContent>
          ) : null}
        </Card>
      </div>
      <Instructions />
    </>
  );
}
