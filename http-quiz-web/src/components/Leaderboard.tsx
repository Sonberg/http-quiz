import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";

type TeamScore = {
  teamId: string;
  teamName: string;
  points: number;
};

type Props = {
  teams: TeamScore[];
};

export function Leaderboard({ teams }: Props) {
  const textSizeClassName = (index: number) => {
    switch (index) {
      case 0:
        return "text-xl";

      case 1:
        return "text-lg";

      default:
        return "";
    }
  };

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>Team</TableHead>
          <TableHead className="w-[100px]">Score</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {teams.map((x, index) => (
          <TableRow key={x.teamId} className={textSizeClassName(index)}>
            <TableCell className="font-medium font-bold">
              {x.teamName} {index === 0 ? "🎉" : null}
            </TableCell>
            <TableCell>{x.points}</TableCell>
          </TableRow>
        ))}
      </TableBody>
      {teams.length ? null : <TableCaption>No teams added yet!</TableCaption>}
    </Table>
  );
}
